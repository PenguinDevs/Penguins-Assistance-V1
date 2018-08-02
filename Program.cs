using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace Penguins_Assistant
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botsToken = "NDczNDE2MTUzNDMyMTk1MDcy.DkMsCg.O5PEz8y7_QZ4Xw76Ki4XInk2K2A";

            //Event Subscriptions!!!!
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            _client.UserLeft += AnnounceUserLeft;

            await RegisterCommandAsync();

            await _client.LoginAsync(TokenType.Bot, botsToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task AnnounceUserLeft(SocketGuildUser user)
        {
            EmbedBuilder builder = new EmbedBuilder();
            var channel = _client.GetChannel(471606148848418827) as SocketTextChannel;
            var guild = user.Guild;

            builder.WithTitle("Oof!")
                .WithDescription($"Nooooo!!! We will miss you, {user.Mention}, you better be back next time! T-T")
                .WithColor(Color.Red)
                .WithImageUrl("https://d26oc3sg82pgk3.cloudfront.net/files/media/uploads/zinnia/2016/10/10/dawsons-creek-tears-how-to-cry-acting-advice-interview-backstage.jpg.644x593_q100.jpg");

            await channel.SendMessageAsync("", false, builder.Build());
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            EmbedBuilder builder = new EmbedBuilder();
            var channel = _client.GetChannel(471606148848418827) as SocketTextChannel;
            var guild = user.Guild;

            builder.WithTitle("Welcome!")
                .WithDescription($"Welcome, {user.Mention}, Enjoy and have fun! Also make sure you read #rules Or say ?rules in #bot-commands")
                .WithColor(Color.Green)
                .WithImageUrl("https://www.learningpotential.gov.au/sites/learningpotential/files/styles/article_image/public/2018/04/back-to-school-for-teens.jpg?itok=IjTy360M");

            await channel.SendMessageAsync("", false, builder.Build());
        }

        private Task Log(LogMessage arg)
        {
            Console.Write(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("?", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
                    
            }
        }
    }
}
