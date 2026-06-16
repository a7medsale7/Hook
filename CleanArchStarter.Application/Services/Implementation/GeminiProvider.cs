using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.FishGuard;
using Hook.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hook.Application.Services.Implementation;

public class GeminiProvider : IAiProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
    }

    public async Task<GeminiClassificationResponse> ClassifyQuestionAsync(string question, CancellationToken cancellationToken = default)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

var systemInstruction = @"You are a classifier for a fishing app (FishGuard AI).
Classify the user's question into ONE of these categories:
- RestrictedLocation: Use this if the user asks IF they are allowed, permitted, or forbidden to fish in a certain place (e.g. 'ممكن اصطاد في', 'مسموح اصطاد', 'هل الصيد ممنوع'). This applies to ANY place, whether specific or a general city.
- LocationAdvice: Use this ONLY if the user asks for recommendations on WHERE to fish or HOW to fish in a general area, NOT asking for permission (e.g. 'ايه احسن اماكن الصيد', 'تنصحني بايه').
- RestrictedTool: Questions asking if a specific tool/gear is allowed or banned.
- FishingSeason: Questions asking when is the allowed time/season to fish.
- GeneralQuestion: Any other general fishing advice.

CRITICAL INSTRUCTION FOR ENTITY EXTRACTION:
When extracting the main 'entity' (location, tool, or season), you MUST correct any typos, misspellings, or slang based on your deep knowledge of Egyptian geography and fishing. 
For example: 
- If the user says 'لاس محمد', extract 'محمية رأس محمد'.
- If the user says 'البللس', extract 'بحيرة البرلس'.
- If the user says 'المنزلة', extract 'بحيرة المنزلة'.
Return the properly spelled, formal Arabic name. If none, return empty string.
Give a confidence score between 0.0 and 1.0.

Return strictly ONLY JSON in this format:
{
  ""category"": ""RestrictedLocation"",
  ""entity"": ""بحيرة البرلس"",
  ""confidence"": 0.95
}";

        var requestBody = new
        {
            system_instruction = new
            {
                parts = new[] { new { text = systemInstruction } }
            },
            contents = new[]
            {
                new { parts = new[] { new { text = question } } }
            },
            generationConfig = new
            {
                responseMimeType = "application/json"
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            return new GeminiClassificationResponse { Category = "GeneralQuestion", Confidence = 0 };
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        
        using var jsonDoc = JsonDocument.Parse(responseString);
        var candidates = jsonDoc.RootElement.GetProperty("candidates");
        if (candidates.GetArrayLength() > 0)
        {
            var text = candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Trim();
                if (text.StartsWith("```json", StringComparison.OrdinalIgnoreCase))
                {
                    text = text.Substring(7);
                }
                else if (text.StartsWith("```", StringComparison.OrdinalIgnoreCase))
                {
                    text = text.Substring(3);
                }
                
                if (text.EndsWith("```"))
                {
                    text = text.Substring(0, text.Length - 3);
                }
                text = text.Trim();

                try
                {
                    return JsonSerializer.Deserialize<GeminiClassificationResponse>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                           ?? new GeminiClassificationResponse { Category = "GeneralQuestion", Confidence = 0 };
                }
                catch
                {
                    return new GeminiClassificationResponse { Category = "GeneralQuestion", Confidence = 0 };
                }
            }
        }

        return new GeminiClassificationResponse { Category = "GeneralQuestion", Confidence = 0 };
    }

    public async IAsyncEnumerable<string> StreamGenerateResponseAsync(string systemPrompt, string userMessage, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:streamGenerateContent?alt=sse&key={_apiKey}";

        var requestBody = new
        {
            system_instruction = new
            {
                parts = new[] { new { text = systemPrompt } }
            },
            contents = new[]
            {
                new { parts = new[] { new { text = userMessage } } }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var reader = new System.IO.StreamReader(stream);

        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("data: "))
            {
                var data = line.Substring(6);
                if (data == "[DONE]") break;

                string? chunkText = null;
                try
                {
                    using var doc = JsonDocument.Parse(data);
                    if (doc.RootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                    {
                        var candidate = candidates[0];
                        if (candidate.TryGetProperty("content", out var contentObj) && contentObj.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                        {
                            chunkText = parts[0].GetProperty("text").GetString();
                        }
                    }
                }
                catch
                {
                    // Ignore parse errors on chunks
                }

                if (!string.IsNullOrEmpty(chunkText))
                {
                    yield return chunkText;
                }
            }
        }
    }
}
