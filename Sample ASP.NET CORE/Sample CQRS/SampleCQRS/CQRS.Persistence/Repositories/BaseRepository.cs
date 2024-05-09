using System.Linq.Expressions;
using CQRS.Domain.Entities.Base;
using CQRS.Domain.Repositories.Base;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Repositories;

public class BaseRepository<T, TKey> : IBaseRepository<T, TKey>
    where T : BaseEntity<TKey>, IAggregateRoot
{
    private AppDbContext _dbContext;

    protected DbSet<T> DbSet => _dbContext.Set<T>();

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SetRowVersion(T entity, byte[] version)
    {
        _dbContext.Entry(entity).OriginalValues[nameof(entity.RowVersion)] = version;
    }

    public bool IsDbUpdateConcurrencyException(Exception ex)
    {
        return ex is DbUpdateConcurrencyException;
    }

    public IQueryable<T> GetQueryableSet()
    {
        return DbSet;
    }

    public async Task<T?> GetByIdAsync(TKey id, bool disableTracking = false)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Equals(default(TKey)))
        {
            await AddAsync(entity, cancellationToken);
        }
        else
        {
            await UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedDateTime = DateTime.UtcNow;
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedDateTime = DateTime.UtcNow;
        DbSet.Update(entity);
        await Task.CompletedTask;
    }

    public void UpdateRange(List<T> entities, CancellationToken cancellationToken = default)
    {
        DbSet.UpdateRange(entities);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<T1?> FirstOrDefaultAsync<T1>(IQueryable<T1> query)
    {
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T1?> SingleOrDefaultAsync<T1>(IQueryable<T1> query)
    {
        return await query.SingleOrDefaultAsync();
    }

    public async Task<List<T1>> ToListAsync<T1>(IQueryable<T1> query)
    {
        return await query.ToListAsync();
    }

    public async Task BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _dbContext.BulkInsertAsync(entities, cancellationToken: cancellationToken);
    }

    public async Task BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _dbContext.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
    }

    public async Task BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _dbContext.BulkDeleteAsync(entities, cancellationToken: cancellationToken);
    }

    // public async Task BulkInsert(IEnumerable<T> entities, Expression<Func<T, object>> columnNamesSelector)
    // {
    //     _dbContext.BulkInsertAsync(entities, builder => );
    // }

    // public async Task BulkUpdate(IEnumerable<T> entities, Expression<Func<T, object>> columnNamesSelector)
    // {
    //     await _dbContext.Bul
    // }
    //
    // public void BulkDelete(IEnumerable<T> entities)
    // {
    //     throw new NotImplementedException();
    // }
}