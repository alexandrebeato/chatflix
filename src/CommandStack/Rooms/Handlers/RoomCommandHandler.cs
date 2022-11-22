using CommandStack.Rooms.Adapters;
using CommandStack.Rooms.Commands;
using Core.Domain.Handlers;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Domain.Rooms.Repository;
using MediatR;

namespace CommandStack.Rooms.Handlers
{
    public class RoomCommandHandler : CommandHandler, IRequestHandler<CreateRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;

        public RoomCommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IRoomRepository roomRepository) : base(mediator, notifications) =>
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));

        public async Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = request.ToRoom();
            await _roomRepository.Insert(room);
            return true;
        }
    }
}