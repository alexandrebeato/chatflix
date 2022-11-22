using CommandStack.Messages.Events;
using MediatR;

namespace CommandStack.Messages.Handlers
{
    public class MessageEventHandler : INotificationHandler<MessageCreatedEvent>
    {
        public Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Check if message is a command and execute it
            return Task.CompletedTask;
        }
    }
}