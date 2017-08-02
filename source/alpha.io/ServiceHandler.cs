using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace alpha.io
{
    public class ServiceHandler
    {
        private CommandService _commands;
        private DiscordSocketClient _client;


        public async Task Install(DiscordSocketClient c)
        {
            _client = c;
            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                ThrowOnError = true,
#if DEBUG
                DefaultRunMode = RunMode.Sync
#elif RELEASE
                DefaultRunMode = RunMode.Async
#endif
            });
            
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            await InitializeSQLiteAsync(_commands);
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
            var result = await _commands.ExecuteAsync(context, argPos);

            // If the command failed, notify the user
            if (!result.IsSuccess)
                await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }

        public async Task InitializeSQLiteAsync(CommandService commands)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(dbPath);
            using (var db = new SQLite.VoiceDb())
                db.Database.EnsureCreated();
            using (var db = new SQLite.GuildDb())
                db.Database.EnsureCreated();
            using (var db = new SQLite.UserDb())
                db.Database.EnsureCreated();
            using (var db = new SQLite.MessageDb())
                db.Database.EnsureCreated();

        }
    }
}
