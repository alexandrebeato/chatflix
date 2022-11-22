using Core.Domain.Events;

namespace CommandStack.Messages.Events
{
    public class MessageCreatedEvent : Event
    {
        public MessageCreatedEvent(Guid id, string content)
        {
            Id = id;
            Content = content;
        }

        public Guid Id { get; }
        public string Content { get; }
    }
}