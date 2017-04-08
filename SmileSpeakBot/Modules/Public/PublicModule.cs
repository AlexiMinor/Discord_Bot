using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SmileSpeakBot.Modules.Public
{
    public class PublicModule : ModuleBase
    {
        private readonly DictReplacer _dr = new DictReplacer(Dicts.EmojiDictUnicode);


        [Command("ssb-invite")]
        [Summary("Returns the OAuth2 Invite URL of the bot")]
        public async Task Invite()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync(
                $"A user with `MANAGE_SERVER` can invite me to your server here: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot>");
        }


        [Command("smile")]
        [Alias("emo")]
        [Summary("Emoji-echoes the provided input")]
        public async Task Emo([Remainder] string input)
        {
            await ReplyAsync(_dr.Replace(input));
        }

        [Command("ssb-info")]
        [Summary("Returns some tech(and not tech) info")]
        public async Task Info()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync(
                $"{Format.Bold("Info")}\n" +
                $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                $"- Uptime: {GetUptime()}\n\n" +

                $"{Format.Bold("Stats")}\n" +
                $"- Heap Size: {GetHeapSize()} MB\n" +
                $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}" +
                $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}"
            );
        }

        [Command("ssb-help")]
        [Summary("Shows a help message")]
        public async Task Help()
        {
            await ReplyAsync(
                $"{Format.Bold("List of commands")}\n" +
                $"{Format.Bold("!emo")} *text* or {Format.Bold("!smile")} *text* - the purpose, duh.\n" +
                $"{Format.Bold("!ssb-invite")} - If you want me for your server.\n" +
                $"{Format.Bold("!ssb-info")} - If you want to know more about me.\n" +
                $"{Format.Bold("!ssb-help")} - If you want to read this again.\n"
            );
        }

        private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
