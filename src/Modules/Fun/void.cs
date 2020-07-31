using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Modules
{
    [Remarks("Fun")]
    [Summary("The void!!")]
    public class VoidModule : ModuleBase<SocketCommandContext>
    {
        [Command("void")]
        [Alias("empty")]
        [Remarks("[LineCount (default is 100, max is 200)]")]
        public Task Run(int lineCount = 100)
        {
            if (lineCount > 200) lineCount = 200;
            if (lineCount < 1) lineCount = 1;

            return ReplyAsync(String.Concat(Enumerable.Repeat("‎‎‎\n", lineCount)));
        }
    }
}