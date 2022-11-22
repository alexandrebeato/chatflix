using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using MediatR;

namespace Core.Domain.Handlers
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationHandler _notifications;

        protected CommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotificarErro(string nome, string mensagem) =>
            _mediator.RaiseEvent(new DomainNotification(nome, mensagem));

        protected bool HasNotifications() =>
            _notifications.HasNotifications();
    }
}