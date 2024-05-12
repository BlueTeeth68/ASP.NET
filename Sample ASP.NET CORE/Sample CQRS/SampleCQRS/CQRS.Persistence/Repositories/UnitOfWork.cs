using CQRS.Domain.Repositories;

namespace CQRS.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}