using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using NekosSharp;
using Newtonsoft.Json;
using Vensha.Handlers;

namespace Vensha
{
    public class Program
    {
        public static DiscordSocketClient client;
        public static Configuration Config;
        public static NekoClient nekos = new NekoClient("Vensha");
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            client = new DiscordSocketClient();

            client.Log += Log;

            Config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Directory.GetCurrentDirectory() + "/config.json"));

            bool debug = args.Contains("debug");
            if (debug) Console.WriteLine("Logging in in debug mode!");

            await client.LoginAsync(TokenType.Bot, Config.token);
            await client.StartAsync();

            var commands = new CommandHandler(client, debug);
            await commands.initCommands();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
    public class Configuration
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}