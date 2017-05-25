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

        [Command("export")]
        [Summary("exports voice data, syntax 'export DD/MM/YYYY-DD/MM/YYYY' date range")]
        public async Task Export()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync($"You can invite me to your server here: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot> (Requires 'Manage Server' permission)");
        }

        public async Task CreateExport()
        {
            //await _db.VoiceActivity.T
        }
    }
}
