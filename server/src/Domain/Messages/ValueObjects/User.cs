using Core.Domain.Entities;

namespace Domain.Messages.ValueObjects
{
    public class User : ValueObject<User>
    {
        public User(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
    }
}