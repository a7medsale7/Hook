using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;
public class BoatOwnerRepository : IBoatOwnerRepository
{
    private readonly ApplicationDbContext _context;
    public BoatOwnerRepository(ApplicationDbContext context) 
    {
        _context = context;
    }
    public async Task<BoatOwnerProfile?> GetByIdAsync(Guid id)
    {
        return await _context.BoatOwnerProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }
    // التنفيذ الفعلي لجلب بروفايل المالك مع بيانات المستخدم المرتبطة
    public async Task<BoatOwnerProfile?> GetByUserIdAsync(string userId)
    {
        return await _context.BoatOwnerProfiles
            .Include(p => p.User) // جلب بيانات الـ ApplicationUser
            .Include(p => p.Boats) // جلب القوارب التابعة له (للعرض لاحقاً)
            .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);
    }
    // جلب الطلبات المعلقة (Pending) مع فرزها من الأحدث للأقدم
    public async Task<IEnumerable<BoatOwnerProfile>> GetPendingApplicationsAsync()
    {
        return await _context.BoatOwnerProfiles
            .Include(p => p.User) // لنعرض اسم مقدم الطلب وإيميله للأدمن
            .Where(p => p.Status == RequestStatus.Pending && !p.IsDeleted)
            .ToListAsync();
    }
    // جلب القوارب حسب الحالة المختارة وفلترة المحذوفين (Soft Delete)
    public async Task<IEnumerable<BoatOwnerProfile>> GetByStatusAsync(RequestStatus status)
    {
        return await _context.BoatOwnerProfiles
            .Include(p => p.User)
            .Where(p => p.Status == status && !p.IsDeleted)
            .AsNoTracking() // لتحسين الأداء لأن البيانات للعرض فقط (Read-only)
            .ToListAsync();
    }
    // ميثود سريعة للتحقق من وجود بروفايل للمستخدم
    public async Task<bool> HasProfileAsync(string userId)
    {
        return await _context.BoatOwnerProfiles
            .AnyAsync(p => p.UserId == userId && !p.IsDeleted);
    }

    public async Task AddAsync(BoatOwnerProfile profile)
    {
        await _context.BoatOwnerProfiles.AddAsync(profile);
    }

    public void Update(BoatOwnerProfile profile)
    {
        _context.BoatOwnerProfiles.Update(profile);
    }

    public void SoftDelete(BoatOwnerProfile profile)
    {
        profile.IsDeleted = true;
        _context.BoatOwnerProfiles.Update(profile);
    }

    public async Task<IEnumerable<BoatOwnerProfile>> GetAllAsync()
    {
        return await _context.BoatOwnerProfiles
            .Include(p => p.User)
            .Include(p => p.Boats)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<BoatOwnerProfile>> GetDeletedAsync()
    {
        return await _context.BoatOwnerProfiles
            .IgnoreQueryFilters()
            .Include(p => p.User)
            .Include(p => p.Boats)
            .Where(p => p.IsDeleted)
            .ToListAsync();
    }

    public async Task<BoatOwnerProfile?> GetByIdWithDeletedAsync(Guid id)
    {
        return await _context.BoatOwnerProfiles
            .IgnoreQueryFilters()
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}