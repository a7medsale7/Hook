using Hook.Domain.Entities;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;
public interface IBoatOwnerRepository
{
    // جلب بروفايل المالك بدلالة معرف البروفايل (Id)
    Task<BoatOwnerProfile?> GetByIdAsync(Guid id);
    // جلب بروفايل المالك بدلالة معرف المستخدم (UserId)
    Task<BoatOwnerProfile?> GetByUserIdAsync(string userId);
    // جلب كافة الطلبات التي تنتظر المراجعة من الأدمن
    Task<IEnumerable<BoatOwnerProfile>> GetPendingApplicationsAsync();
    // جلب كافة الملاك بناءً على حالتهم (Approved, Pending, Rejected)
    Task<IEnumerable<BoatOwnerProfile>> GetByStatusAsync(RequestStatus status);
    // التحقق فورياً إذا كان المستخدم يمتلك بروفايل مالك قارب أم لا
    Task<bool> HasProfileAsync(string userId);

    Task AddAsync(BoatOwnerProfile profile);
    void Update(BoatOwnerProfile profile);
    void SoftDelete(BoatOwnerProfile profile);
    Task<IEnumerable<BoatOwnerProfile>> GetAllAsync();
    Task<IEnumerable<BoatOwnerProfile>> GetDeletedAsync();
    Task<BoatOwnerProfile?> GetByIdWithDeletedAsync(Guid id);
}
