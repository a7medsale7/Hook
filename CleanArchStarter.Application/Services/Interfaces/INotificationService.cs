using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;
using Hook.Domain.Enums;

namespace Hook.Application.Services.Interfaces;

public interface INotificationService
{
    Task<Result<IEnumerable<NotificationResponse>>> GetUserNotificationsAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<int>> GetUnreadNotificationsCountAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<NotificationResponse>>> GetUnreadNotificationsAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result> MarkAsReadAsync(Guid notificationId, string userId, CancellationToken cancellationToken = default);
    Task CreateNotificationAsync(string userId, string? actorUserId, NotificationType type, Guid? referenceId, CancellationToken cancellationToken = default);
    Task CreateNotificationAsync(string userId, string? actorUserId, NotificationType type, Guid? referenceId, string? message, CancellationToken cancellationToken = default);
}
