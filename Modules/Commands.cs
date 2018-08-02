using Discord.Commands;
using System;
using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pengins_Assistant.Modules
{
    [Group("ping")]
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("user")]
        public async Task PingAsync(SocketGuildUser user)
         {
                await ReplyAsync($"Pong! {user.Mention}");
         }

        [Command("rules")]
        public async Task RulesAsync()
        {
            await ReplyAsync("Here are the rules! Make sure you obey them!!!" +
                "                    :arrow_forward:  NO spamming" +
                "                      :arrow_forward:  NO swearing" +
                "                    :arrow_forward:  NO random tags/mentions (@)" +
                "                      :arrow_forward: All Music Commands To Be Done In #music-commands" +
                "                    :arrow_forward: Dont ask for roles. Staff will hand them out when needed to" +
                "                      :arrow_forward: Rythm 2 is here for the staff, dont use him." +
                "                    :arrow_forward: Please before you leave disconnect Rythm and dont leave him running (!disconnect" +
                "                      If These Are Broken. They can get you warned kicked And maybe worse like a BAN HAMMER");
        }

        [Command("warn")]
        [Summary("Warns a player for thier behaviour")]
        public async Task WarnAsync()
        {
            await ReplyAsync("Oi! Behave" + " for");
        }

        [Command("info")]
        [Summary("Shows the info of the bot")]
        public async Task InfoAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Penguin's Assistance")
                .WithDescription("V1.1 Is here! All hosted on Heroku, running for 24/7, Note that this bot is still in alpha, so please expect Issues, delays and not alot of commands. More commands releasing soon! So keep an eye on our updates!!!")
                .WithColor(Color.Orange);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("mystats")]
        [Summary("Shows the status of a player mentioned")]
        public async Task PlrStatsAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.AddField("User", Context.User.Mention)
                .AddInlineField("Connection Status", Context.Client.GroupChannels)
                .AddInlineField("Playing", Context.User.Game)
                .AddInlineField("Account Created", Context.User.CreatedAt)
                .AddInlineField("Developer Appearance Code/ID", Context.User.AvatarId)
                .AddInlineField("Developer Code/ID", Context.User.Id)
                .AddInlineField("Online/Offline", Context.User.Status)
                .AddInlineField("Is bot?", Context.User.IsBot)
                .AddInlineField("Is Webhooker???", Context.User.IsWebhook)
                .AddInlineField("Player Number Tags", Context.User.Discriminator)
                .AddInlineField("User Name", Context.User.Username)
                .AddInlineField("Player Number Tags #2", Context.User.DiscriminatorValue)
                .AddInlineField("Player Code", $"{Context.User.Username}#{Context.User.Discriminator}")
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("say")]
        [Summary("Repeats what you say")]
        public async Task SayAsync( [Remainder] string text)
        {
            await ReplyAsync($"{text}");
        }


    }
}
