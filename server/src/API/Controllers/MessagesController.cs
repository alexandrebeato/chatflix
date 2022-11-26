using API.Models.Messages;
using AutoMapper;
using CommandStack.Messages.Commands;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Domain.Messages.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : BaseController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public MessagesController(INotificationHandler<DomainNotification> notifications, IUser user, IMediatorHandler mediator, IMessageRepository messageRepository, IMapper mapper) : base(notifications, user, mediator)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("room/{roomId:guid}")]
        public async Task<IActionResult> GetByRoom([FromRoute] Guid roomId) =>
            Response(_mapper.Map<IEnumerable<MessageModel>>(await _messageRepository.GetByRoom(roomId)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMessageModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var userId = _user.GetAuthenticatedUserId();
            var userName = _user.GetAuthenticatedUserName();

            if (string.IsNullOrEmpty(userName))
            {
                NotifyError("User not found", "User not found");
                return Response(model);
            }

            var command = new CreateMessageCommand(model.Id, model.Content, model.RoomId, userId, userName, model.CreatedAt);
            await _mediator.SendCommand(command);

            if (!IsValid())
                return Response();

            return Response(_mapper.Map<MessageModel>(await _messageRepository.GetById(model.Id)));
        }
    }
}