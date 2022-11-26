using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IMongoRepository<T> where T : Entity<T>
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
    }
}