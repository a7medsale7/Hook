using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Services.Interfaces;

public interface IEventService
{
    Task<Result> JoinEventAsync(Guid postId, string userId, CancellationToken cancellationToken = default);
    Task<Result> LeaveEventAsync(Guid postId, string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<EventParticipantResponse>>> GetEventParticipantsAsync(Guid postId, string currentUserId, CancellationToken cancellationToken = default);
}
