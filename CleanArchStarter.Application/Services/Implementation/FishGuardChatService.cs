using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.Ai;
using Hook.Application.Contracts.FishGuard;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace Hook.Application.Services.Implementation;

public class FishGuardChatService : IFishGuardChatService
{
    private readonly IAiProvider _aiProvider;
    private readonly IAiDatabaseMapper _aiDatabaseMapper;
    private readonly IConversationService _conversationService;
    private readonly IMemoryCache _cache;

    public FishGuardChatService(
        IAiProvider aiProvider,
        IAiDatabaseMapper aiDatabaseMapper,
        IConversationService conversationService,
        IMemoryCache cache)
    {
        _aiProvider = aiProvider;
        _aiDatabaseMapper = aiDatabaseMapper;
        _conversationService = conversationService;
        _cache = cache;
    }

    public async IAsyncEnumerable<string> ProcessAndStreamResponseAsync(
        string userId, 
        Guid? conversationId,
        ChatRequestDto request, 
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // 1. Get or Create Conversation
        var conversation = await _conversationService.GetOrCreateConversationAsync(conversationId, userId, request.Message, cancellationToken);
        
        // Save User Message
        await _conversationService.AddMessageAsync(conversation.Id, MessageRole.User, request.Message, false, null, null, cancellationToken);

        var jsonOptions = new JsonSerializerOptions 
        { 
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
        };

        // Yield conversation Id so the client knows it immediately
        yield return JsonSerializer.Serialize(new { conversationId = conversation.Id }, jsonOptions) + "\n";

        // 1.5 Check for Interactive Fallback Reply
        var messagesResult = await _conversationService.GetConversationMessagesAsync(conversation.Id, userId, cancellationToken);
        if (messagesResult.IsSuccess && messagesResult.Value != null)
        {
            var history = messagesResult.Value.OrderBy(m => m.CreatedOn).ToList();
            if (history.Count >= 3)
            {
                var lastAiMsg = history[history.Count - 2];
                var prevUserMsg = history[history.Count - 3];

                if (lastAiMsg.Role == "Assistant" && lastAiMsg.Content.Contains("هل تريدني أن أجيبك بناءً على معلوماتي العامة"))
                {
                    var msgTrimmed = request.Message.Trim().ToLowerInvariant();
                    var affirmativeWords = new[] { "اه", "نعم", "ايوه", "أيوة", "ياريت", "ماشي", "اوك", "ok", "yes", "تمام", "يلا", "بالتأكيد", "أكيد" };
                    var negativeWords = new[] { "لا", "لأ", "شكرا", "لا شكرا", "مش عايز", "no", "بلاش" };

                    bool isYes = affirmativeWords.Any(w => msgTrimmed.Contains(w));
                    bool isNo = negativeWords.Any(w => msgTrimmed.Contains(w));

                    if (isYes && !isNo)
                    {
                        // Proceed with Gemini answering the PREVIOUS question using general knowledge
                        string prompt = BuildSystemPrompt(null, ChatCategory.GeneralQuestion);
                        var responseBuilder = new System.Text.StringBuilder();
                        await foreach (var chunk in _aiProvider.StreamGenerateResponseAsync(prompt, prevUserMsg.Content, cancellationToken))
                        {
                            responseBuilder.Append(chunk);
                            yield return JsonSerializer.Serialize(new { chunk = chunk }, jsonOptions) + "\n";
                        }
                        var fullResponse = responseBuilder.ToString();
                        await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, fullResponse, false, "General", string.Empty, cancellationToken);
                        yield break;
                    }
                    else if (isNo)
                    {
                        var politeResponse = "أنا تحت أمرك في أي وقت يا صديقي، لو احتجت تسأل عن أي قوانين أو أماكن صيد تانية أنا موجود!";
                        yield return JsonSerializer.Serialize(new { chunk = politeResponse }, jsonOptions) + "\n";
                        await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, politeResponse, false, string.Empty, string.Empty, cancellationToken);
                        yield break;
                    }
                    // If neither yes nor no, fall through and treat it as a brand new question!
                }
            }
        }

        // 2. Cache Check (Only for normal questions)
        var cacheKey = $"FishGuard_{request.Message.ToLowerInvariant().Trim()}";
        if (_cache.TryGetValue<string>(cacheKey, out var cachedResponse) && cachedResponse != null)
        {
            yield return JsonSerializer.Serialize(new { chunk = cachedResponse }, jsonOptions) + "\n";
            await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, cachedResponse, true, "Cache", null, cancellationToken);
            yield break;
        }

        // 3. Classification & Database Mapping
        var mappingResult = await _aiDatabaseMapper.MapQuestionToDatabaseAsync(request.Message, cancellationToken);
        
        ChatCategory finalCategory = mappingResult.Category;
        var dbEntities = mappingResult.DbEntities;
        string sourceType = mappingResult.SourceType;

        // 4. If category is DB-related but NO results found → Interactive Fallback
        if ((dbEntities == null || dbEntities.Count == 0) && 
            (finalCategory == ChatCategory.RestrictedLocation || finalCategory == ChatCategory.RestrictedTool || finalCategory == ChatCategory.FishingSeason))
        {
            var interactiveFallback = "للاسف الداتا مش معايا حول هذا الموضوع بالتحديد في قاعدة البيانات الرسمية. هل تريدني أن أجيبك بناءً على معلوماتي العامة كخبير صيد؟";
            yield return JsonSerializer.Serialize(new { chunk = interactiveFallback }, jsonOptions) + "\n";
            
            // Save AI Message to DB
            await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, interactiveFallback, false, string.Empty, string.Empty, cancellationToken);
            yield break;
        }

        // 5. Build system prompt with ALL matched DB entities
        string systemPrompt = BuildSystemPrompt(dbEntities, finalCategory);

        // 6. Stream Response from AI
        var fullResponseBuilderNormal = new System.Text.StringBuilder();
        
        await foreach (var chunk in _aiProvider.StreamGenerateResponseAsync(systemPrompt, request.Message, cancellationToken))
        {
            fullResponseBuilderNormal.Append(chunk);
            yield return JsonSerializer.Serialize(new { chunk = chunk }, jsonOptions) + "\n";
        }

        var fullResponseNormal = fullResponseBuilderNormal.ToString();

        // Save to Cache
        _cache.Set(cacheKey, fullResponseNormal, TimeSpan.FromMinutes(30));

        // Save AI Message to DB
        bool fromDb = dbEntities != null && dbEntities.Count > 0;
        await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, fullResponseNormal, fromDb, sourceType, string.Empty, cancellationToken);
    }

    private string BuildSystemPrompt(List<object>? dbEntities, ChatCategory category)
    {
        var basePrompt = "You are 'FishGuard AI', an expert and highly trusted assistant for the 'HOOK' fishing platform in Egypt. You specialize in Egyptian fishing locations, sustainable practices, and marine regulations. Answer the user strictly using the provided database context if available. Reply in the same language as the user. NEVER mention that you are an AI, a language model, or Gemini. Act entirely as an experienced local Egyptian fishing guide.";

        if (dbEntities == null || dbEntities.Count == 0)
        {
            return basePrompt + " You don't have specific database regulations for this question. Rely on your deep general knowledge about fishing in Egypt. If asked about best places to fish in specific Egyptian cities (like Damanhour, Alexandria, etc.), provide real, specific, well-known local spots (e.g., specific canals like Mahmoudiya Canal, specific beaches, or lakes). Be highly informative, practical, and give detailed tips on what to catch and how, as a true local expert would. Do not give generic advice like 'ask local fishermen'.";
        }

        var contextJson = JsonSerializer.Serialize(dbEntities, new JsonSerializerOptions 
        { 
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true 
        });

        return $@"{basePrompt} 

CRITICAL INSTRUCTION: You found {dbEntities.Count} matching regulation(s) in the database. You MUST use ALL of them to give a comprehensive answer.
If the database context indicates restricted locations, prohibited tools, or closed seasons, YOU MUST EXPLICITLY TELL THE USER IT IS NOT ALLOWED. 
DO NOT encourage fishing or give fishing tips for restricted items. 
Explain the reason for each restriction using ONLY the data provided below.
Present the information in a clear, organized way. If there are multiple seasons or regulations, list them all.

Database Context ({dbEntities.Count} result(s)):
{contextJson}";
    }
}
