using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace Vensha.Modules {
    [Remarks ("Utility")]
    [Summary ("Google something")]

    public class GoogleModule : ModuleBase<SocketCommandContext> {
        [Command ("google")]
        [Alias ("lmgtfy")]
        [Remarks ("<Thing to google>")]
        public Task Run ([Remainder] string search) {
            return ReplyAsync ("https://google.com/search?q=" + Uri.EscapeDataString (search));
        }
    }
}