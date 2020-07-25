using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Vensha {
    class Program {
        private DiscordSocketClient client;
        public static void Main (string[] args) => new Program ().MainAsync ().GetAwaiter ().GetResult ();

        public async Task MainAsync () {
            client = new DiscordSocketClient ();

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            await client.LoginAsync (TokenType.Bot, Config.token);
            await client.StartAsync ();

            await Task.Delay (-1);
        }

        private async Task MessageReceived (SocketMessage message) {
            if (message.Content == $"{Config.prefix}ping") {
                var msg = await message.Channel.SendMessageAsync ("Pong!");
                _ = msg.ModifyAsync (m => m.Content = $"Pong!\nCommand execution latency: `{msg.Timestamp.ToUnixTimeMilliseconds() - message.Timestamp.ToUnixTimeMilliseconds()}ms`.\nSocket latency: `{client.Latency}ms`");
            }

        }

        private Task Log (LogMessage msg) {
            Console.WriteLine (msg.ToString ());
            return Task.CompletedTask;
        }
    }
}