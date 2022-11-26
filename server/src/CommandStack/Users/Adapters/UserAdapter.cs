using CommandStack.Users.Commands;
using Domain.Users;

namespace CommandStack.Users.Adapters
{
    public static class UserAdapter
    {
        public static User ToUser(this CreateUserCommand command) =>
            User.Factory.CreateInstance(command.Id, command.UserName, command.Password, command.CreatedAt);
    }
}