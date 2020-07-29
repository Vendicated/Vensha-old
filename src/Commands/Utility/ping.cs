using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Commands.Utility {
    public class Ping : ModuleBase<SocketCommandContext> {

        [Command ("ping")]
        [Alias ("p", "ms", "latency")]
        [Summary ("Check my latency")]
        public async Task ping () {
            var msg = await ReplyAsync ("Pinging...");
            _ = msg.ModifyAsync (m => m.Content = $"Pong!\nCommand latency: `{msg.Timestamp.ToUnixTimeMilliseconds() - Context.Message.Timestamp.ToUnixTimeMilliseconds()}ms`\nWebsocket latency: `{Context.Client.Latency}ms`");
        }
    }
}