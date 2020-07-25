using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Vensha {
    public class Program {
        private DiscordSocketClient client;
        public static void Main (string[] args) => new Program ().MainAsync ().GetAwaiter ().GetResult ();

        public async Task MainAsync () {
            client = new DiscordSocketClient ();

            client.Log += Log;

            await client.LoginAsync (TokenType.Bot, Config.token);
            await client.StartAsync ();

            await Task.Delay (-1);

            var commands = new Handlers.CommandHandler (client);
            await commands.initCommands ();
        }

        private Task Log (LogMessage msg) {
            Console.WriteLine (msg.ToString ());
            return Task.CompletedTask;
        }

    }
}