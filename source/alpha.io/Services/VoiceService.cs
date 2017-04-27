using System;
using System.Threading.Tasks;
using alpha.io.SQLite;
using alpha.io.SQLite.Entities.Voice;
using Discord.WebSocket;

namespace alpha.io.Services
{
    public class VoiceService
    {

        private VoiceDb _db;

        public VoiceService()
        {
            _db = new VoiceDb();
        }

        public async Task UserStateChanged(SocketUser user, SocketVoiceState initialState, SocketVoiceState newState)
        {

            if (!initialState.IsDeafened && newState.IsDeafened)
            {
                //server deafened
                //not logging these
                return;
            }

            if (!initialState.IsMuted && newState.IsMuted)
            {
                //server muted
                //not logging these
                return;
            }

            if (initialState.IsDeafened && !newState.IsDeafened)
            {
                //server undeafened
                //not logging these
                return;
            }

            if (initialState.IsMuted && !newState.IsMuted)
            {
                //server unmuted
                //not logging these
                return;
            }

            var newActivity = new LiteVoiceActivity(newState.VoiceChannel?.Guild.Id ?? initialState.VoiceChannel.Guild.Id, new ulong(), user.Id, LiteVoiceActivity.ActivityTypes.Empty);

            if (!initialState.IsSelfMuted && newState.IsSelfMuted)
            {
                newActivity.ChannelId = newState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Mute; //muted
            }
            if (initialState.IsSelfMuted && !newState.IsSelfMuted)
            {
                newActivity.ChannelId = newState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Unmute; //unmuted
            }
            if (!initialState.IsSelfDeafened && newState.IsSelfDeafened)
            {
                newActivity.ChannelId = newState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Deafen;
            }
            if (initialState.IsSelfDeafened && !newState.IsSelfDeafened)
            {
                newActivity.ChannelId = newState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Undeafen;
            }

            if (newActivity.Activity != LiteVoiceActivity.ActivityTypes.Empty)
            {
                await InsertAsync(newActivity);
                return;
            }
           
            if (initialState.VoiceChannel == null && newState.VoiceChannel != null)
            {
                newActivity.ChannelId = newState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Connected;

                //Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {user.Username} connected to channel {newState.VoiceChannel.Name}");
                //TODO: User connects to voice
            }
            if (initialState.VoiceChannel != null && newState.VoiceChannel != null)
            {
                newActivity.ChannelId = newState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Moved;

                //Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {user.Username} moved to channel {newState.VoiceChannel.Name}");
                //TODO: User moved voice channels
            }
            if (newState.VoiceChannel == null)
            {
                newActivity.ChannelId = initialState.VoiceChannel.Id;
                newActivity.Activity = LiteVoiceActivity.ActivityTypes.Disconnected;

                //Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {user.Username} disconnected from channel {initialState.VoiceChannel?.Name}");
                //TODO: User disconnected
            }
            //var actArray = new[] { "Disconnected", "Connected", "Moved", "TimedOut" };
            //Console.WriteLine($"[VoiceActivity] User: {user.Id} <{actArray[newActivity.ActivityId]}> {(newActivity.ActivityId > 0 ? "to room: " + newState.VoiceChannel?.Name.Replace("?", "") : "from room: " + initialState.VoiceChannel?.Name).Trim()}");
            await InsertAsync(newActivity);
        }

        private async Task InsertAsync(LiteVoiceActivity activity)
        {
            await _db.AddVoiceActivityAsync(activity);
        }
    }
}
