using Core.Domain.Entities;

namespace Domain.Rooms
{
    public class Room : Entity<Room>
    {
        private Room(Guid id, string name, DateTime createdAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
        }

        public string Name { get; private set; }

        public static class Factory
        {
            public static Room CreateInstance(Guid id, string name, DateTime createdAt) =>
                new(id, name, createdAt);
        }
    }
}