using Core.Domain.Events;

namespace CommandStack.Messages.Events
{
    public class MessageCreatedEvent : Event
    {
        public MessageCreatedEvent(Guid id, Guid roomId, string content)
        {
            Id = id;
            RoomId = roomId;
            Content = content;
        }

        public Guid Id { get; }
        public Guid RoomId { get; }
        public string Content { get; }
    }
}