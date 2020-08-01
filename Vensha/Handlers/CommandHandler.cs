using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Vensha.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly bool debug;

        public CommandHandler(DiscordSocketClient _client, bool _debug = false)
        {
            commands = new CommandService();
            client = _client;

            commands.Log += Log;

            debug = _debug;
        }

        public async Task initCommands()
        {
            client.MessageReceived += handleCommands;

            await commands.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), null);
        }

        private async Task handleCommands(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasStringPrefix(Program.config.prefix, ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;

            var context = new SocketCommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos, null);

            if (result.ErrorReason == "Unknown command.") return;

            if (!result.IsSuccess)
                _ = context.Channel.SendMessageAsync($"An Error occurred:\n```cs\n{result}```");
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}