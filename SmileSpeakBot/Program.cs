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
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No token provided. Exiting");
                return;
            }
            _token = args[0];
            new Program().Start().GetAwaiter().GetResult();
        }

        private static string _token;
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task Start()
        {
            // Define the DiscordSocketClient
            _client = new DiscordSocketClient();

            // Login and connect to Discord.
            await _client.LoginAsync(TokenType.Bot, _token);
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
