using Core.Domain.Events;

namespace Core.Domain.Commands
{
    public abstract class Command : Notification
    {
        protected DateTime Timestamp { get; private set; }

        public Command() =>
            Timestamp = DateTime.UtcNow;
    }
}