using Core.Domain.Commands;

namespace CommandStack.Rooms.Commands
{
    public class CreateRoomCommand : Command
    {
        public CreateRoomCommand(Guid id, string name, DateTime createdAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; }
    }
}