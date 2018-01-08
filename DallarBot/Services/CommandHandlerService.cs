﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

using DallarBot.Classes;

namespace DallarBot.Services
{
    public class CommandHandlerService
    {
        public DiscordSocketClient discord;
        private CommandService commands;
        private IServiceProvider provider;
        private readonly SettingsHandlerService settings;

        public async Task InitializeAsync(IServiceProvider _provider)
        {
            provider = _provider;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public CommandHandlerService(IServiceProvider _provider, DiscordSocketClient _discord, CommandService _commands, SettingsHandlerService _settings)
        {
            discord = _discord;
            commands = _commands;
            provider = _provider;
            settings = _settings;

            discord.MessageReceived += HandleCommandAsync;

            discord.SetGameAsync(settings.dallarSettings.startup.taskName);
        }

        private async Task HandleCommandAsync(SocketMessage socketMessage)
        {
            var message = socketMessage as SocketUserMessage;

            if (message == null)
            {
                return;
            }

            var context = new SocketCommandContext(discord, message);                       
            int argPos = 0;

            if (message.HasCharPrefix('!', ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, provider);
                if (result.Error == CommandError.MultipleMatches)
                {
                    await context.Message.DeleteAsync();
                    await context.User.SendMessageAsync("There are multiple users with that name, please mention the person you are sending to with an @.");
                }
            }
        }
    }
}