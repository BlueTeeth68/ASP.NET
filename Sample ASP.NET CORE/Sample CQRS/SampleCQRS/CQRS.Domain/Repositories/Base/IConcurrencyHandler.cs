namespace CQRS.Domain.Repositories.Base;

//handle concurrency in  EF core
public interface IConcurrencyHandler<TEntity>
{
    void SetRowVersion(TEntity entity, byte[] version);
    
    bool IsDbUpdateConcurrencyException(Exception ex);
}