using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.BoatOwner;
public class ApplyBoatOwnerRequest
{
    public string NationalIdNumber { get; set; } = string.Empty;
    public IFormFile NationalIdImage { get; set; } = null!;
    public string BoatLicenseNumber { get; set; } = string.Empty;
    public IFormFile BoatLicenseImage { get; set; } = null!;

    public string? InstaPayNumber { get; set; }
    public string? VodafoneCashNumber { get; set; }
}
