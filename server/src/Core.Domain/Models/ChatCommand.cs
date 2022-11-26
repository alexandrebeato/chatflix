namespace Core.Domain.Models
{
    public class ChatCommand
    {
        public ChatCommand(string command, Guid? roomId)
        {
            Command = command;
            RoomId = roomId;
            Arguments = new List<string>();
        }

        public string Command { get; private set; }
        public Guid? RoomId { get; private set; }
        public List<string>? Arguments { get; private set; }

        public void AddArgument(string argument)
        {
            if (Arguments == null)
                Arguments = new List<string>();

            Arguments.Add(argument);
        }
    }
}