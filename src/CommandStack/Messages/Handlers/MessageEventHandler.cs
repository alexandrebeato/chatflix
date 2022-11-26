using CommandStack.Messages.Events;
using Core.Domain.Interfaces;
using Core.Domain.Utils;
using MediatR;

namespace CommandStack.Messages.Handlers
{
    public class MessageEventHandler : INotificationHandler<MessageCreatedEvent>
    {
        private readonly IQueueService _queueService;

        public MessageEventHandler(IQueueService queueService) =>
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));

        public async Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
        {
            var chatCommands = notification.Content.ExtractChatCommands(notification.RoomId);

            if (chatCommands == null)
                return;

            foreach (var chatCommand in chatCommands)
                switch (chatCommand.Command.ToLower())
                {
                    case "stock":
                        await _queueService.SendAsync(chatCommand, "stock");
                        break;
                }
        }
    }
}