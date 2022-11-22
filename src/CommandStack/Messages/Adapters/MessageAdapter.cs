using CommandStack.Messages.Commands;
using CommandStack.Messages.Events;
using Domain.Messages;

namespace CommandStack.Messages.Adapters
{
    public static class MessageAdapter
    {
        public static Message ToMessage(this CreateMessageCommand command) =>
            Message.Factory.CreateInstance(command.Id, command.RoomId, command.UserId, command.UserName, command.Content, command.CreatedAt);

        public static MessageCreatedEvent ToMessageCreatedEvent(this Message message) =>
            new MessageCreatedEvent(message.Id, message.Content);
    }
}