using MediatR;

namespace Core.Domain.Events
{
    public abstract class Notification : IRequest<bool>
    {
        public string NotificationType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Notification() =>
            NotificationType = GetType().Name;
    }
}