using Core.Domain.Interfaces;

namespace Domain.Messages.Repository
{
    public interface IMessageRepository : IMongoRepository<Message>
    {
        Task<List<Message>> GetByRoom(Guid roomId);
    }
}