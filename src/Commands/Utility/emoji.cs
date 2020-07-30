using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Commands {
    [Remarks ("Utility")]
    [Summary ("Get the link to one or more emotes")]

    public class EmojiModule : ModuleBase<SocketCommandContext> {

        private Regex reg = new Regex (@"\<?(?<animated>a)?:?(?<name>\w{2,32}):(?<id>\d{17,19})\>?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [Command ("emoji")]
        [Alias ("e", "emote")]

        public Task Run ([Remainder] string args) {

            var matches = reg.Matches (Context.Message.Content);

            if (matches.Count == 0)
                return ReplyAsync ("You did not provide any valid emojis.");

            var urls = matches.Select (match => $"https://cdn.discordapp.com/emojis/{match.Groups["id"].ToString()}.{(match.Groups["animated"].Success ? "gif" : "png")}");

            string output = urls.Count () == 1 ? urls.First () : String.Join ('\n', urls.Distinct ().Select (url => $"<{url}>"));

            if (output.Length > 2000) return ReplyAsync ("Please do not add this many emojis at once.");

            return ReplyAsync (output);
        }
    }
}