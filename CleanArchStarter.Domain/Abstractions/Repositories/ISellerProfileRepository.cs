using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories
{
    public interface ISellerProfileRepository
    {
        Task<bool> HasProfileAsync(string userId);
        Task<SellerProfile?> GetByUserIdAsync(string userId);
        Task<SellerProfile?> GetByIdAsync(Guid id);
        Task<IEnumerable<SellerProfile>> GetPendingApplicationsAsync();
        Task<IEnumerable<SellerProfile>> GetAllAsync();
        Task<IEnumerable<SellerProfile>> GetDeletedAsync();
        Task<SellerProfile?> GetByIdWithDeletedAsync(Guid id);
        Task AddAsync(SellerProfile profile);
        void Update(SellerProfile profile);
        void SoftDelete(SellerProfile profile);
    }
}
