using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Vensha.Configuration;
using Vensha.Extensions;
using Vensha.Handlers;

namespace Vensha {
    public class Program {
        private DiscordSocketClient client;
        public static void Main (string[] args) => new Program ().MainAsync ().GetAwaiter ().GetResult ();

        public async Task MainAsync () {
            client = new DiscordSocketClient ();

            client.Log += Log;

            await client.LoginAsync (TokenType.Bot, Config.token);
            await client.StartAsync ();

            var commands = new CommandHandler (client);
            await commands.initCommands ();

            await Task.Delay (-1);

        }

        private Task Log (LogMessage msg) {
            Console.WriteLine (msg.ToString ());
            return Task.CompletedTask;
        }

    }
}