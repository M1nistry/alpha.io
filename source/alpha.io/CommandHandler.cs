using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace alpha.io
{
    public class CommandHandler
    {
        private CommandService _commands;
        private DiscordSocketClient _client;
        private IDependencyMap map;

        public async Task Install(IDependencyMap _map)
        {
            _client = _map.Get<DiscordSocketClient>();
            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                ThrowOnError = true
            });
            //_map.Add(_commands);
            map = _map;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommand;
        }


        public async Task HandleCommand(SocketMessage socketMessage)
        {
            var message = socketMessage as SocketUserMessage;
            if (message == null) return;

            // Mark where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message has a valid prefix, adjust argPos 
            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix('!', ref argPos))) return;

            // Create a Command Context
            var context = new CommandContext(_client, message);
            // Execute the Command, store the result
            var result = await _commands.ExecuteAsync(context, argPos, map);

            // If the command failed, notify the user
            if (!result.IsSuccess)
                await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }
    }
}
