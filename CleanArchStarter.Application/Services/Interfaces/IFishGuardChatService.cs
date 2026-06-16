using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.FishGuard;

namespace Hook.Application.Services.Interfaces;

public interface IFishGuardChatService
{
    IAsyncEnumerable<string> ProcessAndStreamResponseAsync(string userId, Guid? conversationId, ChatRequestDto request, CancellationToken cancellationToken = default);
}
