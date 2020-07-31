using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Vensha.Modules
{
    [Remarks("Utility")]
    [Summary("You are here ;)")]

    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private CommandService _service;
        public HelpModule(CommandService service) => _service = service;
        [Command("help")]
        [Alias("h")]
        [Remarks("[Commandname]")]
        public Task Run()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Help menu")
                .WithDescription($"View a list of commands below. For info on a specific command, use `{Program.Config.prefix}help [CommandName]`");

            foreach (string category in _service.Modules.Select(mod => mod.Remarks).Distinct().Where(c => c != "Development"))
            {
                var commands = _service.Commands
                    .Where(cmd => cmd.Module.Remarks == category)
                    .Select(cmd => $"`{Program.Config.prefix}{cmd.Name}` - {cmd.Module.Summary ?? "No description provided."}")
                    .Distinct();

                embed.AddField(category, String.Join('\n', commands));
            }

            return ReplyAsync("", false, embed.Build());
        }

        [Command("help")]
        [Alias("h")]
        [Remarks("[Commandname]")]
        public Task Run(string arg)
        {
            var command = _service.Commands.FirstOrDefault(c => c.Name.ToLower() == arg.ToLower() || c.Aliases.Contains(arg.ToLower()));

            if (command == null || command.Module.Remarks == "Development") return ReplyAsync($"`{arg}` is not a valid command.");

            var embed = new EmbedBuilder()
                .WithTitle(Program.Config.prefix + command.Name)
                .WithDescription(command.Module.Summary ?? "No description provided.")
                .AddField("Aliases", command.Aliases.Count > 0 ? String.Join(", ", command.Aliases) : $"{command.Name} has no aliases.")
                .AddField("Usage", $"{Program.Config.prefix}{command.Name} {command.Remarks}");

            return ReplyAsync("", false, embed.Build());
        }
    }
}