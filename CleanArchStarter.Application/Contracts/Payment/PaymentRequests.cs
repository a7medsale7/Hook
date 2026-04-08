using Hook.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace Hook.Application.Contracts.Payment;

public record UploadReceiptRequest(IFormFile ReceiptImage);

public record VerifyPaymentRequest(bool IsApproved, string? Notes);

public record PaymentFilterRequest(
    PaymentStatus? Status = null,
    PaymentMethod? Method = null,
    DateTime? Date = null
);
