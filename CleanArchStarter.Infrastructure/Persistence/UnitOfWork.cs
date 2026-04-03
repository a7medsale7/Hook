using CleanArchStarter.Domain.Abstractions;
using CleanArchStarter.Infrastructure.Persistence;

namespace CleanArchStarter.Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
