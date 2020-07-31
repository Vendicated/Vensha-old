using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Modules {
    [Remarks ("Fun")]
    [Summary ("The void!!")]

    public class Module : ModuleBase<SocketCommandContext> {
        [Command ("void")]
        [Alias ("empty")]
        [Remarks ("[LineCount (default is 100, max is 200)]")]
        public Task Run (int lineCount = 100) {
            if (lineCount > 200) lineCount = 100;
            return ReplyAsync ("‎‎‎" + new String ('\n', lineCount) + "‎‎‎");
        }
    }
}