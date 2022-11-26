using Core.Domain.Commands;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using MediatR;

namespace Core.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator) =>
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public Task RaiseEvent<T>(T @event, CancellationToken cancellation = default) where T : Event =>
            _mediator.Publish(@event, cancellation);

        public Task SendCommand<T>(T command, CancellationToken cancellation = default) where T : Command =>
            _mediator.Send(command, cancellation);
    }
}