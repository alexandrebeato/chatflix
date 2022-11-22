using Core.Domain.Interfaces;

namespace Domain.Users.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserName(string userName);
    }
}