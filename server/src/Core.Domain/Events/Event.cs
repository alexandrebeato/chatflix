using MediatR;

namespace Core.Domain.Events
{
    public abstract class Event : Notification, INotification
    {
        protected DateTime Timestamp { get; private set; }

        protected Event() =>
            Timestamp = DateTime.UtcNow;
    }
}