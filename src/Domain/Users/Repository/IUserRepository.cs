using Core.Domain.Interfaces;

namespace Domain.Users.Repository
{
    public interface IUserRepository : IMongoRepository<User>
    {
        Task<User> GetByUserName(string userName);
    }
}