using System;
using System.ComponentModel.DataAnnotations;
using alpha.io.Services;

namespace alpha.io.SQLite.Entities.Voice
{
    public class LiteVoiceActivity : LiteEntity<ulong>
    {
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        [Required]
        public ulong GuildId { get; set; }
        [Required]
        public ulong UserId { get; set; }
        [Required]
        public ulong ChannelId { get; set; }
        [EnumDataType(typeof(ActivityTypes))]
        public ActivityTypes Activity { get; set; }

        public enum ActivityTypes
        {
            Empty = -1,
            Disconnected = 0,
            Connected = 1,
            Moved = 2,
            TimedOut = 3,
            Mute = 4,
            Unmute = 5,
            Deafen = 6,
            Undeafen = 7
        }

        public LiteVoiceActivity(ulong guildId, ulong userId, ulong channelId, ActivityTypes activity)
        {
            Timestamp = DateTime.UtcNow;
            GuildId = guildId;
            UserId = userId;
            ChannelId = channelId;
            Activity = activity;
        }
    }
}
