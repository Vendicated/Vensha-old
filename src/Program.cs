using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Vensha.Handlers;

namespace Vensha {
    public class Program {
        private DiscordSocketClient client;
        public static Configuration Config;
        public static void Main (string[] args) => new Program ().MainAsync ().GetAwaiter ().GetResult ();

        public async Task MainAsync () {
            client = new DiscordSocketClient ();

            client.Log += Log;

            Config = JsonConvert.DeserializeObject<Configuration> (File.ReadAllText (Directory.GetCurrentDirectory () + "/config.json"));

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
    public class Configuration {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}