using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.FishGuard;

namespace Hook.Application.Services.Interfaces;

public interface IAiProvider
{
    Task<GeminiClassificationResponse> ClassifyQuestionAsync(string question, CancellationToken cancellationToken = default);
    IAsyncEnumerable<string> StreamGenerateResponseAsync(string systemPrompt, string userMessage, CancellationToken cancellationToken = default);
}
