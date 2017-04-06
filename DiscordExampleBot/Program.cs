using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace SmileSpeakBot
{
    public class Program
    {
        // Convert our sync main to an async main.
        public static void Main(string[] args) =>
            new Program().Start(args).GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task Start(string[] args)
        {
            // Define the DiscordSocketClient
            _client = new DiscordSocketClient();
            if (args[0] == null)
            {
                await Log(new LogMessage(LogSeverity.Critical, "Start", "No token provided. Exiting"));
                return;
            }
            var token = args[0];

            // Login and connect to Discord.
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            var map = new DependencyMap();
            map.Add(_client);

            _handler = new CommandHandler();
            await _handler.Install(map);

            // Block this program until it is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
