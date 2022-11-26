using CommandStack.Messages.Adapters;
using CommandStack.Messages.Commands;
using Core.Domain.Handlers;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Domain.Messages.Repository;
using MediatR;

namespace CommandStack.Messages.Handlers
{
    public class MessageCommandHandler : CommandHandler, IRequestHandler<CreateMessageCommand, bool>,
                                                         IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;

        public MessageCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IMessageRepository messageRepository) : base(mediator, notifications) =>
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));

        public async Task<bool> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = request.ToMessage();
            await _messageRepository.Insert(message);
            await _mediator.RaiseEvent(message.ToMessageCreatedEvent());
            return true;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetById(request.Id);

            if (message is null)
            {
                await _mediator.RaiseEvent(new DomainNotification(request.NotificationType, "Message not found."));
                return false;
            }

            await _messageRepository.Delete(message.Id);
            return true;
        }
    }
}