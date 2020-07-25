using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Commands.Utility {
    public class InfoModule : ModuleBase<SocketCommandContext> {
        [Command ("ping")]
        [Alias ("p", "ms", "latency")]
        [Summary ("Check my latency")]
        public async Task ping () {
            var msg = await Context.Channel.SendMessageAsync ("Pinging...");
            _ = msg.ModifyAsync (m => m.Content = $"Pong!\nCommand latency: `{Context.Message.Timestamp.ToUnixTimeMilliseconds() - msg.Timestamp.ToUnixTimeMilliseconds()}ms`\nWebsocket latency: `{Context.Client.Latency}ms`");
        }
    }
}