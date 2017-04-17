using System;
using System.IO;
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace alpha.io
{
    public class AlphaBot
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public static void Main(string[] args) => new AlphaBot().Start().GetAwaiter().GetResult();
        
        
        public async Task Start()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                ConnectionTimeout = 15000
            });

            var keyString = File.ReadAllText(Environment.CurrentDirectory + @"\Key.txt");
            await _client.LoginAsync(TokenType.Bot, keyString);
            await _client.StartAsync();

            var map = new DependencyMap();
            map.Add(_client);
            _handler = new CommandHandler();
            await _handler.Install(map);

            _client.UserVoiceStateUpdated += new VoiceWatcher().UserStateChanged;
            _client.Log += Logger;
            _client.Ready += async delegate
            {
                await ClientOnReady();
            };
            await Task.Delay(-1);
        }

        public async Task ClientOnReady()
        {
            //TODO: Format into table with server information (users, date joined, etc)
            string servers = string.Join(", ", (from guild in _client.Guilds select guild.Name).ToArray());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now,-19} [Info   ] Gateway: {servers}");
            await _client.SetGameAsync("Alpha Crucis Exiles");
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
