using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Commands.Fun {
    public class HowHaram : ModuleBase<SocketCommandContext> {
        private Random _random = new Random ();

        [Command ("howharam")]
        [Alias ("howhalal", "haram", "halal")]
        [Summary ("Find out how haram something is")]
        public Task Run (Discord.IUser user) {
            return Result (user?.Username);
        }

        [Command ("howharam")]
        [Alias ("howhalal", "haram", "halal")]
        [Summary ("Find out how haram something is")]
        public Task Run ([Remainder] string arg = null) {
            return Result (arg ?? Context.User.Username);
        }

        private Task Result (string arg) {
            int haramLevel = _random.Next (101);
            int barLevel = (int) Math.Round ((double) haramLevel / 5);

            var embed = new Discord.EmbedBuilder ()
                .WithTitle ("Haram Meter")
                .WithDescription ($"{arg} is {haramLevel}% haram.\n`Halal` {new String ('-', barLevel)}🔵{new String ('-', 20 - barLevel)} `Haram`");

            ReplyAsync ("", false, embed.Build ());

            return Task.CompletedTask;
        }

    }
}