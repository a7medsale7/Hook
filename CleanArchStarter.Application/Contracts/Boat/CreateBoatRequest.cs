using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Boat;
public class CreateBoatRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }

    // لإرسال الصور من الـ Front-end
    public IFormFileCollection? Images { get; set; }
    public int MainImageIndex { get; set; } = 0;
}
