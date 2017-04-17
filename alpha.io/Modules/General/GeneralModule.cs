using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace alpha.io.Modules.General
{
    public class GeneralModule : ModuleBase
    {

        [Command("invite")]
        [Summary("Returns the invite URL for the bot")]
        public async Task Invite()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync($"You can invite me to your server here: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot> (Requires 'Manage Server' permission)");
        }

        [Command("leave")]
        [Summary("Prompts the bot to leave this guild.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task Leave()
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("This command can only be ran in a server.");
                return;
            }
            await ReplyAsync("Farewell!");
            await Context.Guild.LeaveAsync();
        }

        [Command("sync")]
        [Summary("Checks if the data is synced and if not resyncs")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task Sync()
        {
            var voiceChannels = await Context.Guild.GetVoiceChannelsAsync();
            var guildMembers = await Context.Guild.GetUsersAsync();
            foreach (var channel in voiceChannels)
            {
                
            }
        }

        [Command("info")]
        public async Task Info()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync(
                $"{Format.Bold("Info")}\n" +
                $"Author : {application.Owner.Username} (ID {application.Owner.Id}\n" +
                $"Library: Discord.Net ({DiscordConfig.Version})\n" +
                $"Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture} \n" +
                $"Uptime : {GetUptime()}\n\n");
    }

        private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
    }
}
