using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading;
using System.Threading.Tasks;

namespace alpha.io
{
    public class Program
    {

        public static DiscordSocketClient Client { get; set; }
        private readonly IDependencyMap _map = new DependencyMap();
        private readonly CommandService _commands = new CommandService();

        public static void Main(string[] args)
        {
            var alphaBot = new AlphaBot();
            Client = new AlphaBot().AlphaClient();
            new Program().MainASync().GetAwaiter().GetResult();
        }
        
        public async Task MainASync()
        {
            Client.Log += Logger;
            await InitCommands();

            var keyString = File.ReadAllText(Environment.CurrentDirectory + @"\Key.txt");
            await Client.LoginAsync(TokenType.Bot, keyString);
            await Client.StartAsync();
            Client.Ready += async delegate
            {
                await ClientOnReady();
            };
            await Task.Delay(-1);
        }

        public async Task ClientOnReady()
        {
            //TODO: Format into table with server information (users, date joined, etc)
            string servers = string.Join(", ", (from guild in Client.Guilds select guild.Name).ToArray());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now,-19} [Info   ] Gateway: {servers}");
        }

        private async Task InitCommands()
        {
            //_map.Add();
            Client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            int pos = 0;

            if (msg.HasCharPrefix('+', ref pos) || msg.HasMentionPrefix(msg.Discord.CurrentUser, ref pos))
            {
                var context = new SocketCommandContext(msg.Discord, msg);

                var result = await _commands.ExecuteAsync(context, pos, _map);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    Console.WriteLine($"Command Error: {context.Message}");
            }
        }

        public Task Logger(LogMessage message)
        {
            var cc = Console.ForegroundColor;
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,-7}] {message.Source}: {message.Message}");
            Console.ForegroundColor = cc;
            return null;
        }
    }
}
