using Core.Domain.Commands;

namespace CommandStack.Messages.Commands
{
    public class CreateMessageCommand : Command
    {
        public CreateMessageCommand(Guid id, string content, Guid roomId, Guid userId, string userName, DateTime createdAt)
        {
            Id = id;
            Content = content;
            RoomId = roomId;
            UserId = userId;
            UserName = userName;
            CreatedAt = createdAt;
        }

        public Guid Id { get; }
        public string Content { get; }
        public Guid RoomId { get; }
        public Guid UserId { get; }
        public string UserName { get; }
        public DateTime CreatedAt { get; }
    }
}