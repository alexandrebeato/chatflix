using Core.Domain.Interfaces;
using Core.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;
        protected Guid UserId { get; set; }
        protected string? UserName { get; set; }

        protected BaseController(INotificationHandler<DomainNotification> notifications, IUser user, IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;

            if (user.IsAuthenticated())
            {
                UserId = user.GetAuthenticatedUserId();
                UserName = user.GetAuthenticatedUserName();
            }
        }

        protected new IActionResult Response(object? result = null)
        {
            if (!ModelState.IsValid)
                NotifyModelStateErrors();

            if (IsValid())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                errors = _notifications.GetNotifications().Select(p => p.Value)
            });
        }

        protected bool IsValid() =>
            (!_notifications.HasNotifications());

        protected void NotifyModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(p => p.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, errorMessage);
            }
        }

        protected void NotifyError(string code, string message) =>
            _mediator.RaiseEvent(new DomainNotification(code, message));
    }
}