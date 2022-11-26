using CommandStack.Users.Adapters;
using CommandStack.Users.Commands;
using Core.Domain.Handlers;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Domain.Users.Repository;
using MediatR;

namespace CommandStack.Users.Handlers
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IUserRepository userRepository) : base(mediator, notifications) =>
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToUser();
            await _userRepository.Insert(user);
            return true;
        }
    }
}