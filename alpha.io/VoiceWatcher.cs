using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace alpha.io
{
    public class VoiceWatcher : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public async Task UserStateChanged(SocketUser user, SocketVoiceState initialState, SocketVoiceState newState)
        {
            if (initialState.VoiceChannel == null)
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {user.Username} connected to channel {newState.VoiceChannel.Name}");
                //TODO: User connects to voice
            }
            if (initialState.VoiceChannel != null && newState.VoiceChannel != null)
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {user.Username} moved to channel {newState.VoiceChannel.Name}");
                //TODO: User moved voice channels
            }
            if (newState.VoiceChannel == null)
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] {user.Username} disconnected from channel {initialState.VoiceChannel?.Name}");
                //TODO: User disconnected
            }
        }
    }
}
