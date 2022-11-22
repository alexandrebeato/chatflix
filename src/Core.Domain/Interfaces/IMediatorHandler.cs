using Core.Domain.Commands;
using Core.Domain.Events;

namespace Core.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task RaiseEvent<T>(T @event, CancellationToken cancellation = default) where T : Event;
        Task SendCommand<T>(T command, CancellationToken cancellation = default) where T : Command;
    }
}