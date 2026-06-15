using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;
using Hook.Domain.Enums;

namespace Hook.Application.Services.Interfaces;

public interface IComplaintService
{
    Task<Result> SupportComplaintAsync(Guid postId, string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ComplaintResponse>>> GetComplaintsForAdminAsync(int page, int pageSize, ComplaintStatus? status, string currentUserId, CancellationToken cancellationToken = default);
    Task<Result> ResolveComplaintAsync(Guid postId, string adminId, ResolveComplaintRequest request, CancellationToken cancellationToken = default);
}
