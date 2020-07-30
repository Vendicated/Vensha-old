using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Vensha.Config;

namespace Vensha.Commands {
    [Remarks ("Utility")]
    [Summary ("You are here ;)")]

    public class HelpModule : ModuleBase<SocketCommandContext> {

        private CommandService _service;

        public HelpModule (CommandService service) => _service = service;

        [Command ("help")]
        [Alias ("h")]
        [Remarks ("[Commandname]")]
        public Task Run () {
            var embed = new EmbedBuilder ().WithTitle ("Help menu");

            foreach (string category in _service.Modules.Select (mod => mod.Remarks).Distinct ()) {
                var commands = _service.Commands.Where (cmd => cmd.Module.Remarks == category);
                embed.AddField (category, String.Join ('\n', commands.Select (cmd => $"`{Configuration.prefix}{cmd.Name}` - {cmd.Summary ?? "No description provided."}").Distinct ()));
            }

            return ReplyAsync ("", false, embed.Build ());
        }

        [Command ("help")]
        [Alias ("h")]
        [Remarks ("[Commandname]")]
        public Task Run ([Remainder] string arg) {
            var command = _service.Commands.FirstOrDefault (c => c.Name.ToLower () == arg.ToLower () || c.Aliases.Contains (arg.ToLower ()));

            if (command == null) return ReplyAsync ($"`{arg}` is not a valid command.");

            var embed = new EmbedBuilder ()
                .WithTitle (Configuration.prefix + command.Name)
                .WithDescription (command.Module.Summary ?? "No description provided.")
                .AddField ("Aliases", command.Aliases.Count > 0 ? String.Join (", ", command.Aliases) : $"{command.Name} has no aliases.")
                .AddField ("Usage", $"{Configuration.prefix}{command.Name} {command.Remarks}");

            return ReplyAsync ("", false, embed.Build ());
        }
    }
} 