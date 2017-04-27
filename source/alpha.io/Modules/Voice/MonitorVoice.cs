using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alpha.io.SQLite;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace alpha.io.Modules.Voice
{
    class MonitorVoice : ModuleBase<SocketCommandContext>
    {
        private VoiceDb _db;

        [Command("monitor", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        [Summary("Toggles the monitoring of voice activity within this guild")]
        public async Task Monitor()
        {
            
        }
    }
}
