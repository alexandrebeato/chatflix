using Core.Domain.Commands;

namespace CommandStack.Messages.Commands
{
    public class DeleteMessageCommand : Command
    {
        public DeleteMessageCommand(Guid id) =>
            Id = id;

        public Guid Id { get; }
    }
}