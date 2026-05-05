using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Payment;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IPaymentService
{
    Task<Result<PaymentResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PaymentResponse>>> GetMyPaymentsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PaymentResponse>>> GetFilteredPaymentsAsync(PaymentFilterRequest filter, string? userId = null, Guid? ownerId = null, CancellationToken cancellationToken = default);
    Task<Result<PaymentResponse>> UploadReceiptAsync(Guid id, string userId, UploadReceiptRequest request, CancellationToken cancellationToken = default);
    Task<Result<PaymentResponse>> VerifyPaymentAsync(Guid id, string userId, VerifyPaymentRequest request, CancellationToken cancellationToken = default);
    Task<Result<PaymentStatsResponse>> GetFinancialStatsAsync(string? userId = null, Guid? ownerId = null, CancellationToken cancellationToken = default);
    Task<Result> MarkAsRefundedAsync(Guid id, string userId, CancellationToken cancellationToken = default);
}
