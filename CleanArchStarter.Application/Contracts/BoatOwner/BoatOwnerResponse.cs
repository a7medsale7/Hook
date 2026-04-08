using Hook.Domain.Enums;
using System;

namespace Hook.Application.Contracts.BoatOwner;

public class BoatOwnerResponse
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NationalIdNumber { get; set; } = string.Empty;
    public string NationalIdPhotoUrl { get; set; } = string.Empty;
    public string BoatLicenseNumber { get; set; } = string.Empty;
    public string BoatLicensePhotoUrl { get; set; } = string.Empty;
    public string? InstaPayNumber { get; set; }
    public string? VodafoneCashNumber { get; set; }
    public RequestStatus Status { get; set; }
    public string? AdminRejectionReason { get; set; }
    public DateTime CreatedOn { get; set; }
}
