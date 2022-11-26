using API.Models.Rooms;
using AutoMapper;
using CommandStack.Rooms.Commands;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Domain.Rooms.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/rooms")]
    public class RoomsController : BaseController
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomsController(INotificationHandler<DomainNotification> notifications, IUser user, IMediatorHandler mediator, IRoomRepository roomRepository, IMapper mapper) : base(notifications, user, mediator)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Response(_mapper.Map<IEnumerable<RoomModel>>(await _roomRepository.GetAll()));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var command = _mapper.Map<CreateRoomCommand>(model);
            await _mediator.SendCommand(command);

            if (!IsValid())
                return Response();

            return Response(_mapper.Map<RoomModel>(await _roomRepository.GetById(model.Id)));
        }
    }
}