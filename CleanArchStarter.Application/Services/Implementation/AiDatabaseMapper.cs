using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.Ai;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hook.Application.Services.Implementation;

public class AiDatabaseMapper : IAiDatabaseMapper
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AiDatabaseMapper(ApplicationDbContext context, HttpClient httpClient, IConfiguration configuration)
    {
        _context = context;
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
    }

    private class GeminiMapperResponse
    {
        public string Category { get; set; } = "GeneralQuestion";
        public List<string> Keywords { get; set; } = new();
    }

    private string NormalizeArabic(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;
        return text.Replace("ة", "ه")
                   .Replace("أ", "ا")
                   .Replace("إ", "ا")
                   .Replace("آ", "ا")
                   .Replace("ى", "ي")
                   .Trim();
    }

    private bool FuzzyContains(string source, string keyword)
    {
        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(keyword)) return false;
        var normalizedSource = NormalizeArabic(source);
        var normalizedKeyword = NormalizeArabic(keyword);
        return normalizedSource.Contains(normalizedKeyword) || normalizedKeyword.Contains(normalizedSource);
    }

    public async Task<AiMapperResult> MapQuestionToDatabaseAsync(string question, CancellationToken cancellationToken = default)
    {
        // ── Step 1: Ask Gemini to classify the question and extract search keywords ──
        var classificationResult = await ClassifyWithGeminiAsync(question, cancellationToken);

        if (!Enum.TryParse<ChatCategory>(classificationResult.Category, out var category))
        {
            return new AiMapperResult { Category = ChatCategory.GeneralQuestion };
        }

        // If it's a general question or location advice, no DB search needed
        if (category == ChatCategory.GeneralQuestion || category == ChatCategory.LocationAdvice)
        {
            return new AiMapperResult { Category = category };
        }

        var keywords = classificationResult.Keywords;
        if (keywords == null || keywords.Count == 0)
        {
            return new AiMapperResult { Category = category };
        }

        // ── Step 2: Search the database ourselves using the extracted keywords ──
        var result = new AiMapperResult
        {
            Category = category,
            SearchKeyword = string.Join(", ", keywords)
        };

        // Search across ALL relevant tables based on category
        if (category == ChatCategory.FishingSeason)
        {
            var allSeasons = await _context.FishingSeasons.AsNoTracking().ToListAsync(cancellationToken);
            foreach (var season in allSeasons)
            {
                foreach (var keyword in keywords)
                {
                    // Match by season name
                    if (FuzzyContains(season.SeasonName, keyword))
                    {
                        if (!result.DbEntities.Any(e => e is Hook.Domain.Entities.FishingSeason fs && fs.Id == season.Id))
                            result.DbEntities.Add(season);
                        break;
                    }
                    // Match by restricted fish species
                    if (season.RestrictedFishSpecies != null && season.RestrictedFishSpecies.Any(fish => FuzzyContains(fish, keyword)))
                    {
                        if (!result.DbEntities.Any(e => e is Hook.Domain.Entities.FishingSeason fs && fs.Id == season.Id))
                            result.DbEntities.Add(season);
                        break;
                    }
                    // Match by banned tools
                    if (season.BannedTools != null && season.BannedTools.Any(tool => FuzzyContains(tool, keyword)))
                    {
                        if (!result.DbEntities.Any(e => e is Hook.Domain.Entities.FishingSeason fs && fs.Id == season.Id))
                            result.DbEntities.Add(season);
                        break;
                    }
                    // Match by region
                    if (FuzzyContains(season.Region, keyword))
                    {
                        if (!result.DbEntities.Any(e => e is Hook.Domain.Entities.FishingSeason fs && fs.Id == season.Id))
                            result.DbEntities.Add(season);
                        break;
                    }
                }
            }
            result.SourceType = "FishingSeason";
        }
        else if (category == ChatCategory.RestrictedTool)
        {
            var allTools = await _context.RestrictedTools.AsNoTracking().ToListAsync(cancellationToken);
            foreach (var tool in allTools)
            {
                foreach (var keyword in keywords)
                {
                    if (FuzzyContains(tool.Name, keyword) || FuzzyContains(tool.Type, keyword) || FuzzyContains(tool.Material, keyword) || FuzzyContains(tool.Description, keyword))
                    {
                        if (!result.DbEntities.Any(e => e is Hook.Domain.Entities.RestrictedTool rt && rt.Id == tool.Id))
                            result.DbEntities.Add(tool);
                        break;
                    }
                }
            }
            result.SourceType = "RestrictedTool";
        }
        else if (category == ChatCategory.RestrictedLocation)
        {
            var allLocations = await _context.RestrictedLocations.AsNoTracking().ToListAsync(cancellationToken);
            foreach (var loc in allLocations)
            {
                foreach (var keyword in keywords)
                {
                    if (FuzzyContains(loc.Name, keyword) || FuzzyContains(loc.Description, keyword))
                    {
                        if (!result.DbEntities.Any(e => e is Hook.Domain.Entities.RestrictedLocation rl && rl.Id == loc.Id))
                            result.DbEntities.Add(loc);
                        break;
                    }
                }
            }
            result.SourceType = "RestrictedLocation";
        }

        return result;
    }

    private async Task<GeminiMapperResponse> ClassifyWithGeminiAsync(string question, CancellationToken cancellationToken)
    {
        var systemInstruction = @"You are a precise classifier and keyword extractor for a fishing app in Egypt.

TASK 1 - CLASSIFY the user's question into ONE of these categories:
- RestrictedLocation: Questions about whether fishing is allowed/prohibited at a specific place.
- LocationAdvice: Questions asking for recommendations on WHERE to fish (not about restrictions).
- RestrictedTool: Questions about whether a specific fishing tool/gear is allowed or banned.
- FishingSeason: Questions about fishing seasons, banned periods, or which fish species are restricted during certain times.
- GeneralQuestion: Any other general fishing advice.

TASK 2 - EXTRACT KEYWORDS: Extract ALL important search keywords from the question. These are the specific names of:
- Fish species (e.g. القاروص, البلطي, البوري, الجمبري)
- Locations (e.g. رأس محمد, بحيرة البرلس, النيل)
- Tools (e.g. شبكة, سنارة, جلب)
- Seasons or time periods

IMPORTANT: Fix any typos or misspellings in the keywords. For example:
- 'لاس محمد' → 'رأس محمد'
- 'البللس' → 'البرلس'
- 'قاروس' → 'القاروص'

Return strictly ONLY JSON in this format:
{
  ""category"": ""FishingSeason"",
  ""keywords"": [""القاروص""]
}";

        var requestBody = new
        {
            system_instruction = new { parts = new[] { new { text = systemInstruction } } },
            contents = new[] { new { parts = new[] { new { text = question } } } },
            generationConfig = new { responseMimeType = "application/json" }
        };

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync(url, content, cancellationToken);
            if (!response.IsSuccessStatusCode)
                return new GeminiMapperResponse();
        }
        catch (Exception)
        {
            return new GeminiMapperResponse();
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        try
        {
            using var jsonDoc = JsonDocument.Parse(responseString);
            var candidates = jsonDoc.RootElement.GetProperty("candidates");
            if (candidates.GetArrayLength() > 0)
            {
                var text = candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Trim();
                    if (text.StartsWith("```json", StringComparison.OrdinalIgnoreCase)) text = text.Substring(7);
                    else if (text.StartsWith("```", StringComparison.OrdinalIgnoreCase)) text = text.Substring(3);
                    if (text.EndsWith("```")) text = text.Substring(0, text.Length - 3);
                    
                    var parsed = JsonSerializer.Deserialize<GeminiMapperResponse>(text.Trim(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (parsed != null) return parsed;
                }
            }
        }
        catch { }

        return new GeminiMapperResponse();
    }
}
