using Core.Domain.Interfaces;

namespace Domain.Messages.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetByRoom(Guid roomId);
    }
}