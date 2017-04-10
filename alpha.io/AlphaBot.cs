using Discord;
using Discord.WebSocket;

namespace alpha.io
{
    
    public class AlphaBot
    {
        public DiscordSocketClient Client { get; set; }
        public static AlphaBot _this;
        public AlphaBot()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            _this = this;
        }

        public DiscordSocketClient AlphaClient()
        {
            return Client;
        }

        public static AlphaBot Bot()
        {
            return _this;
        }
    }
}
