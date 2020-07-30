using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Vensha.Configuration;

namespace Vensha.Handlers {
    public class CommandHandler {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        public CommandHandler (DiscordSocketClient _client, CommandService _commands = null) {
            commands = _commands ?? new CommandService ();
            client = _client;
        }

        public async Task initCommands () {
            client.MessageReceived += handleCommands;

            await commands.AddModulesAsync (System.Reflection.Assembly.GetEntryAssembly (), null);
        }

        private async Task handleCommands (SocketMessage messageParam) {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix (Config.prefix, ref argPos) || message.HasMentionPrefix (client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;

            var context = new SocketCommandContext (client, message);

            var result = await commands.ExecuteAsync (context, argPos, null);

            if (!result.IsSuccess && result.ErrorReason != "Unknown command.")
                await context.Channel.SendMessageAsync ($"An Error occurred:\n```cs\n{result}```");
        }
    }
}