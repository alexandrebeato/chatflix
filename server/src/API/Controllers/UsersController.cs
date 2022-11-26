using API.Configurations;
using API.Models.Users;
using AutoMapper;
using CommandStack.Users.Commands;
using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using Core.Domain.Utils;
using Domain.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(INotificationHandler<DomainNotification> notifications, IUser user, IMediatorHandler mediator, IUserRepository userRepository, IMapper mapper, IConfiguration configuration) : base(notifications, user, mediator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var user = await _userRepository.GetByUserName(model.UserName);

            if (user is not null)
            {
                NotifyError(nameof(user), "User already exists");
                return Response(model);
            }

            model.Password = Cryptography.EncodeWithMD5(model.Password);

            var command = _mapper.Map<CreateUserCommand>(model);
            await _mediator.SendCommand(command);

            if (!IsValid())
                return Response();

            var userModel = _mapper.Map<UserModel>(await _userRepository.GetById(model.Id));

            return Response(new
            {
                token = AuthenticationConfiguration.CreateAuthenticationToken(_configuration, userModel),
                user = model
            });
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var user = await _userRepository.GetByUserName(model.UserName);

            if (user is null)
            {
                NotifyError(nameof(user), "Invalid password");
                return Response(model);
            }

            if (user.Password != Cryptography.EncodeWithMD5(model.Password))
            {
                NotifyError(nameof(user), "Invalid password");
                return Response(model);
            }

            var userModel = _mapper.Map<UserModel>(user);

            return Response(new
            {
                token = AuthenticationConfiguration.CreateAuthenticationToken(_configuration, userModel),
                user = userModel
            });
        }
    }
}