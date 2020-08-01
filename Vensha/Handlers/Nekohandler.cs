using NekosSharp;

namespace Vensha.Handlers
{
    public class Nekohandler
    {
        private NekoClient client;
        public Nekohandler(Discord.WebSocket.DiscordSocketClient Vensha) => client = new NekoClient(Vensha.CurrentUser.Username);
    }
}