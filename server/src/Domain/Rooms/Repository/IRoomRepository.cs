using Core.Domain.Interfaces;

namespace Domain.Rooms.Repository
{
    public interface IRoomRepository : IMongoRepository<Room> { }
}