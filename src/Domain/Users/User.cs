using Core.Domain.Entities;

namespace Domain.Users
{
    public class User : Entity<User>
    {
        private User(Guid id, string userName, string password, DateTime createdAt)
        {
            Id = id;
            UserName = userName;
            Password = password;
            CreatedAt = createdAt;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }

        public static class Factory
        {
            public static User CreateInstance(Guid id, string userName, string password, DateTime createdAt) =>
                new(id, userName, password, createdAt);

            public static User CreateNewUser(string userName, string password) =>
                new(CreateId(), userName, password, DateTime.UtcNow);
        }
    }
}