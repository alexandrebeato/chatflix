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
        private readonly IUser _user;

        public MessageCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IMessageRepository messageRepository, IUser user) : base(mediator, notifications)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

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

            if (message.User.Id != _user.GetAuthenticatedUserId())
            {
                await _mediator.RaiseEvent(new DomainNotification(request.NotificationType, "You are not allowed to delete this message."));
                return false;
            }

            await _messageRepository.Delete(message.Id);
            return true;
        }
    }
}