using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Application.Contracts.FishGuard;

namespace Hook.Application.Services.Interfaces;

public interface IConversationService
{
    Task<ChatConversation> GetOrCreateConversationAsync(Guid? conversationId, string userId, string firstMessage, CancellationToken cancellationToken = default);
    Task AddMessageAsync(Guid conversationId, MessageRole role, string content, bool fromDatabase, string? sourceType, string? sourceId, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<ConversationResponseDto>>> GetUserConversationsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ConversationResponseDto>>> GetStarredConversationsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ChatMessageResponseDto>>> GetConversationMessagesAsync(Guid conversationId, string userId, CancellationToken cancellationToken = default);
    Task<Result> ToggleStarAsync(Guid conversationId, string userId, CancellationToken cancellationToken = default);
    Task<Result> DeleteConversationAsync(Guid conversationId, string userId, CancellationToken cancellationToken = default);
}
