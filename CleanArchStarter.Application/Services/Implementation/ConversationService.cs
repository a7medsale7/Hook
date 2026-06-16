using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.FishGuard;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class ConversationService : IConversationService
{
    private readonly ApplicationDbContext _context;

    public ConversationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChatConversation> GetOrCreateConversationAsync(Guid? conversationId, string userId, string firstMessage, CancellationToken cancellationToken = default)
    {
        if (conversationId.HasValue)
        {
            var existing = await _context.ChatConversations
                .FirstOrDefaultAsync(c => c.Id == conversationId.Value && c.UserId == userId, cancellationToken);
            if (existing != null) return existing;
        }

        var newConversation = new ChatConversation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = firstMessage.Length > 50 ? firstMessage.Substring(0, 47) + "..." : firstMessage,
            LastMessageAt = DateTime.UtcNow,
            MessagesCount = 0
        };

        _context.ChatConversations.Add(newConversation);
        await _context.SaveChangesAsync(cancellationToken);
        
        return newConversation;
    }

    public async Task AddMessageAsync(Guid conversationId, MessageRole role, string content, bool fromDatabase, string? sourceType, string? sourceId, CancellationToken cancellationToken = default)
    {
        var conversation = await _context.ChatConversations.FindAsync(new object[] { conversationId }, cancellationToken);
        if (conversation == null) return;

        var message = new ChatMessage
        {
            ConversationId = conversationId,
            Role = role,
            Content = content,
            FromDatabase = fromDatabase,
            SourceType = sourceType,
            SourceId = sourceId
        };

        _context.ChatMessages.Add(message);
        
        conversation.LastMessageAt = DateTime.UtcNow;
        conversation.MessagesCount += 1;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result<IEnumerable<ConversationResponseDto>>> GetUserConversationsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var conversations = await _context.ChatConversations
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.LastMessageAt)
            .Select(c => new ConversationResponseDto
            {
                Id = c.Id,
                Title = c.Title,
                LastMessageAt = c.LastMessageAt,
                IsStarred = c.IsStarred,
                MessagesCount = _context.ChatMessages.Count(m => m.ConversationId == c.Id && m.Role == MessageRole.User)
            })
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<ConversationResponseDto>>(conversations);
    }

    public async Task<Result<IEnumerable<ChatMessageResponseDto>>> GetConversationMessagesAsync(Guid conversationId, string userId, CancellationToken cancellationToken = default)
    {
        var conversation = await _context.ChatConversations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == conversationId && c.UserId == userId, cancellationToken);

        if (conversation == null)
            return Result.Failure<IEnumerable<ChatMessageResponseDto>>(new Error("ConversationNotFound", "Conversation not found or unauthorized"));

        var messages = await _context.ChatMessages
            .AsNoTracking()
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.CreatedOn)
            .Select(m => new ChatMessageResponseDto
            {
                Id = m.Id,
                Role = m.Role.ToString(),
                Content = m.Content,
                CreatedOn = m.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<ChatMessageResponseDto>>(messages);
    }

    public async Task<Result<IEnumerable<ConversationResponseDto>>> GetStarredConversationsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var conversations = await _context.ChatConversations
            .AsNoTracking()
            .Where(c => c.UserId == userId && c.IsStarred)
            .OrderByDescending(c => c.LastMessageAt)
            .Select(c => new ConversationResponseDto
            {
                Id = c.Id,
                Title = c.Title,
                LastMessageAt = c.LastMessageAt,
                IsStarred = c.IsStarred,
                MessagesCount = _context.ChatMessages.Count(m => m.ConversationId == c.Id && m.Role == MessageRole.User)
            })
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<ConversationResponseDto>>(conversations);
    }

    public async Task<Result> ToggleStarAsync(Guid conversationId, string userId, CancellationToken cancellationToken = default)
    {
        var conversation = await _context.ChatConversations
            .FirstOrDefaultAsync(c => c.Id == conversationId && c.UserId == userId, cancellationToken);

        if (conversation == null)
            return Result.Failure(new Error("ConversationNotFound", "Conversation not found or unauthorized"));

        conversation.IsStarred = !conversation.IsStarred;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteConversationAsync(Guid conversationId, string userId, CancellationToken cancellationToken = default)
    {
        var conversation = await _context.ChatConversations
            .FirstOrDefaultAsync(c => c.Id == conversationId && c.UserId == userId, cancellationToken);

        if (conversation == null)
            return Result.Failure(new Error("ConversationNotFound", "Conversation not found or unauthorized"));

        _context.ChatConversations.Remove(conversation);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
