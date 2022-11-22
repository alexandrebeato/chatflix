using Domain.Users;
using Domain.Users.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration, IMongoClient mongoClient) : base(configuration, mongoClient) { }

        public Task<User> GetByUserName(string userName) =>
            _mongoCollection.Find(p => p.UserName == userName).FirstOrDefaultAsync();
    }
}