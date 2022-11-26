using Core.Domain.Entities;
using Domain.Messages.ValueObjects;

namespace Domain.Messages
{
    public class Message : Entity<Message>
    {
        private Message(Guid id, Guid roomId, Guid userId, string userName, string content, DateTime createdAt)
        {
            Id = id;
            RoomId = roomId;
            User = new User(userId, userName);
            Content = content;
            CreatedAt = createdAt;
        }

        public Guid RoomId { get; private set; }
        public User User { get; private set; }
        public string Content { get; private set; }

        public static class Factory
        {
            public static Message CreateInstance(Guid id, Guid roomId, Guid userId, string userName, string content, DateTime createdAt) =>
                new(id, roomId, userId, userName, content, createdAt);
        }
    }
}