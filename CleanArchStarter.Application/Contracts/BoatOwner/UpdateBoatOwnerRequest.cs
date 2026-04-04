using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.BoatOwner;
public class UpdateBoatOwnerRequest
{
    public string? NationalIdNumber { get; set; }
   public IFormFile? NationalIdImage { get; set; }
    public string? BoatLicenseNumber { get; set; }
    public IFormFile? BoatLicenseImage { get; set; }
}
