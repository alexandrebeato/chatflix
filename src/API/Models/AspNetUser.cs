using System.Security.Claims;
using Core.Domain.Interfaces;

namespace API.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor) =>
            _accessor = accessor;

        public string Identity => _accessor.HttpContext.User.Identity.Name;

        public Guid GetAuthenticatedUserId() =>
            IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value) : Guid.Empty;

        public string? GetAuthenticatedUserName() =>
            IsAuthenticated() ? _accessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value : null;

        public IEnumerable<Claim> GetPermissions() =>
            _accessor.HttpContext.User.Claims;

        public bool IsAuthenticated()
        {
            var isAuthenticated = false;

            try
            {
                isAuthenticated = _accessor.HttpContext.User.Identity.IsAuthenticated;
            }

            catch
            {
                isAuthenticated = false;
            }

            return isAuthenticated;
        }
    }
}