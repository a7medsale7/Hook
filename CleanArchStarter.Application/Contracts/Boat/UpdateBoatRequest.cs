using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Boat;
public class UpdateBoatRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    // صور جديدة حابب يضيفها
    public IFormFileCollection? NewImages { get; set; }
    // IDs الصور اللي عايز يمسحها من الداتا بيز (لو المالك قرر يمسح صورة معينة)
    public List<Guid>? ImageIdsToDelete { get; set; }
    public Guid? MainImageId { get; set; }
}
