using Domain.Rooms;
using Domain.Rooms.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Data
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(IConfiguration configuration, IMongoClient mongoClient) : base(configuration, mongoClient) { }
    }
}