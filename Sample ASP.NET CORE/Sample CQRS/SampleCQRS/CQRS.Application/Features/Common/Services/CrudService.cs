using CQRS.Domain.Entities.Base;
using CQRS.Domain.Exceptions;
using CQRS.Domain.Repositories;
using CQRS.Domain.Repositories.Base;

namespace CQRS.Application.Features.Common.Services;

public class CrudService<T, TKey> : ICrudService<T, TKey> where T : BaseEntity<TKey>, IAggregateRoot
{
    private IUnitOfWork _unitOfWork;
    private IBaseRepository<T, TKey> _repository;

    public CrudService(IUnitOfWork unitOfWork, IBaseRepository<T, TKey> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<List<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.ToListAsync(_repository.GetQueryableSet());
    }

    public async Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null)
            throw new NotFoundException($"Entity {id} not found.");
        return result;
    }

    public Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}