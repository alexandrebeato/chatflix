using Core.Domain.Commands;

namespace CommandStack.Users.Commands
{
    public class CreateUserCommand : Command
    {
        public CreateUserCommand(Guid id, string userName, string password, DateTime createdAt)
        {
            Id = id;
            UserName = userName;
            Password = password;
            CreatedAt = createdAt;
        }

        public Guid Id { get; }
        public string UserName { get; }
        public string Password { get; }
        public DateTime CreatedAt { get; }
    }
}