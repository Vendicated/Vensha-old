using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using NekosSharp;
using Newtonsoft.Json;
using Vensha.Handlers;
using Vensha.Types;

namespace Vensha
{
    public class Program
    {
        public static DiscordSocketClient client;
        public static Configuration config;
        public static Nekohandler nekos;
        public static TwitterHandler twitter;
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            client = new DiscordSocketClient();

            client.Log += Log;

            config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Directory.GetCurrentDirectory() + "/config.json"));

            bool debug = args.Contains("debug");
            if (debug) Console.WriteLine("Logging in in debug mode!");

            await client.LoginAsync(TokenType.Bot, config.token);
            await client.StartAsync();

            var commands = new CommandHandler(client, debug);
            await commands.initCommands();

            client.Ready += InitHandlers;

            await Task.Delay(-1);

        }

        private Task InitHandlers()
        {
            twitter = new TwitterHandler(config);
            nekos = new Nekohandler(client);

            Log(new LogMessage(LogSeverity.Info, "Handlers", "Successfully initialised the handlers!"));

            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}