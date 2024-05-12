using CQRS.Domain.Entities.Base;

namespace CQRS.Application.Features.Common.Services;

public interface ICrudService<T, TKey> where T: BaseEntity<TKey>, IAggregateRoot 
{
    Task<List<T>> GetAsync(CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}