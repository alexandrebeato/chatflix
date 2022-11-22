using Core.Domain.Interfaces;

namespace Domain.Messages.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetByRoom(Guid roomId);
    }
}