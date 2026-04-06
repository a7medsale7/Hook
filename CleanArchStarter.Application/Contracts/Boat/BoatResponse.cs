using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Boat;
public class BoatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    // بيانات المالك (مفيدة للعرض في صفحة التفاصيل)
    public Guid OwnerProfileId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    // لستة روابط الصور عشان الـ Front-end يعرضها
    public List<string> ImageUrls { get; set; } = new();
    public string? MainImageUrl { get; set; }
}