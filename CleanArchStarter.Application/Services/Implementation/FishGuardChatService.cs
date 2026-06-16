using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.FishGuard;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace Hook.Application.Services.Implementation;

public class FishGuardChatService : IFishGuardChatService
{
    private readonly IAiProvider _aiProvider;
    private readonly IKeywordDetectionService _keywordDetection;
    private readonly IFuzzySearchService _fuzzySearch;
    private readonly IConversationService _conversationService;
    private readonly IMemoryCache _cache;

    public FishGuardChatService(
        IAiProvider aiProvider,
        IKeywordDetectionService keywordDetection,
        IFuzzySearchService fuzzySearch,
        IConversationService conversationService,
        IMemoryCache cache)
    {
        _aiProvider = aiProvider;
        _keywordDetection = keywordDetection;
        _fuzzySearch = fuzzySearch;
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

        // 2. Cache Check
        var cacheKey = $"FishGuard_{request.Message.ToLowerInvariant().Trim()}";
        if (_cache.TryGetValue<string>(cacheKey, out var cachedResponse) && cachedResponse != null)
        {
            yield return JsonSerializer.Serialize(new { chunk = cachedResponse }, jsonOptions) + "\n";
            await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, cachedResponse, true, "Cache", null, cancellationToken);
            yield break;
        }

        // 3. Keyword Detection & Classification
        ChatCategory finalCategory = ChatCategory.GeneralQuestion;
        string finalEntity = string.Empty;

        var classification = await _aiProvider.ClassifyQuestionAsync(request.Message, cancellationToken);
        
        if (classification.Confidence >= 0.5 && Enum.TryParse<ChatCategory>(classification.Category, out var parsedCat))
        {
            finalCategory = parsedCat;
            finalEntity = classification.Entity;
        }

        // 4. Database Search
        object? dbEntity = null;
        string sourceType = string.Empty;
        string sourceId = string.Empty;

        if (finalCategory != ChatCategory.GeneralQuestion)
        {
            var searchResult = await _fuzzySearch.SearchAsync(finalCategory, finalEntity, cancellationToken);
            dbEntity = searchResult.Entity;
            sourceType = searchResult.SourceType;
            sourceId = searchResult.SourceId;
        }

        // 5. Generate System Prompt based on DB results
        if (dbEntity == null && (finalCategory == ChatCategory.RestrictedLocation || finalCategory == ChatCategory.RestrictedTool || finalCategory == ChatCategory.FishingSeason))
        {
            var hardcodedResponse = "عذراً يا صديقي، لا تتوفر لدي معلومات رسمية أو قيود مسجلة في قاعدة البيانات حالياً حول هذا المكان أو الأداة. يرجى دائماً توخي الحذر والرجوع للقوانين المحلية.";
            yield return JsonSerializer.Serialize(new { chunk = hardcodedResponse }, jsonOptions) + "\n";
            
            // Save to Cache
            _cache.Set(cacheKey, hardcodedResponse, TimeSpan.FromMinutes(30));

            // Save AI Message to DB
            await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, hardcodedResponse, false, string.Empty, string.Empty, cancellationToken);
            yield break;
        }

        string systemPrompt = BuildSystemPrompt(dbEntity, finalCategory);

        // 6. Stream Response from AI
        var fullResponseBuilder = new System.Text.StringBuilder();
        
        await foreach (var chunk in _aiProvider.StreamGenerateResponseAsync(systemPrompt, request.Message, cancellationToken))
        {
            fullResponseBuilder.Append(chunk);
            yield return JsonSerializer.Serialize(new { chunk = chunk }, jsonOptions) + "\n";
        }

        var fullResponse = fullResponseBuilder.ToString();

        // Save to Cache
        _cache.Set(cacheKey, fullResponse, TimeSpan.FromMinutes(30));

        // Save AI Message to DB
        bool fromDb = dbEntity != null;
        await _conversationService.AddMessageAsync(conversation.Id, MessageRole.Assistant, fullResponse, fromDb, sourceType, sourceId, cancellationToken);
    }

    private string BuildSystemPrompt(object? dbEntity, ChatCategory category)
    {
        var basePrompt = "You are 'FishGuard AI', an expert and highly trusted assistant for the 'HOOK' fishing platform in Egypt. You specialize in Egyptian fishing locations, sustainable practices, and marine regulations. Answer the user strictly using the provided database context if available. Reply in the same language as the user. NEVER mention that you are an AI, a language model, or Gemini. Act entirely as an experienced local Egyptian fishing guide.";

        if (dbEntity == null)
        {
            return basePrompt + " You don't have specific database regulations for this question. Rely on your deep general knowledge about fishing in Egypt. If asked about best places to fish in specific Egyptian cities (like Damanhour, Alexandria, etc.), provide real, specific, well-known local spots (e.g., specific canals like Mahmoudiya Canal, specific beaches, or lakes). Be highly informative, practical, and give detailed tips on what to catch and how, as a true local expert would. Do not give generic advice like 'ask local fishermen'.";
        }

        var contextJson = JsonSerializer.Serialize(dbEntity);
        return $@"{basePrompt} 

CRITICAL INSTRUCTION: You found a matching regulation in the database. You MUST enforce it strictly.
If the database context indicates a restricted location, prohibited tool, or closed season, YOU MUST EXPLICITLY TELL THE USER IT IS NOT ALLOWED. 
DO NOT encourage fishing or give fishing tips for restricted locations. 
Explain the reason for the restriction using ONLY the data provided below.

Database Context:
{contextJson}";
    }
}
