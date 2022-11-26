using Domain.Messages;
using Domain.Messages.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Data
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(IConfiguration configuration, IMongoClient mongoClient) : base(configuration, mongoClient) { }

        public Task<List<Message>> GetByRoom(Guid roomId) =>
            _mongoCollection.Find(x => x.RoomId == roomId).ToListAsync();
    }
}