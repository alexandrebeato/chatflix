using Core.Domain.Models;

namespace Core.Domain.Utils
{
    public static class ChatCommandsExtractor
    {
        public static List<ChatCommand>? ExtractChatCommands(this string content, Guid? roomId)
        {
            if (!content.StartsWith('/'))
                return null;

            var chatCommands = new List<ChatCommand>();

            foreach (var commands in content.Split('/'))
            {
                var command = commands.Split('=');

                if (command.Length < 2)
                    continue;

                var chatCommand = new ChatCommand(command[0], roomId);

                foreach (var argument in command[1].Split(','))
                    chatCommand.AddArgument(argument.Trim());

                chatCommands.Add(chatCommand);
            }

            return chatCommands;
        }
    }
}