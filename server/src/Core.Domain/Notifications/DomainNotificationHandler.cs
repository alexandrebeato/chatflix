using MediatR;

namespace Core.Domain.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler() =>
            _notifications = new List<DomainNotification>();

        public virtual List<DomainNotification> GetNotifications() =>
            _notifications;

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual bool HasNotifications() =>
            _notifications.Any();

        public virtual void ClearNotifications() =>
            _notifications.Clear();

        public void Dispose() =>
            _notifications = new List<DomainNotification>();
    }
}