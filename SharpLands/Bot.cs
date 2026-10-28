using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using SharpLands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Windows.Input;

public class Bot
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _services;
    

    public Bot(
        DiscordSocketClient client,
        InteractionService interactions,
        IServiceProvider services)
    {
        _client = client;
        _interactions = interactions;
        _services = services;
    }


    public async Task StartAsync()
    {

        _client.Log += Log;
        _client.Ready += Ready;
        _client.InteractionCreated += HandleInteraction;


        Console.WriteLine("Conectando...");


        string path = Path.Combine(AppContext.BaseDirectory, "Config", "config.json");
        string json = File.ReadAllText(path);
        Config config = JsonSerializer.Deserialize<Config>(json)!;


        await _client.LoginAsync(TokenType.Bot, config.Token);


        await _interactions.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);

        await _client.StartAsync();


        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }

    private async Task Ready()
    {
        Console.WriteLine($"Bot online! ({_client.CurrentUser.Username})");
        await _interactions.RegisterCommandsToGuildAsync(543844510317150259); //Laboratory
       


    }

    private async Task HandleInteraction(SocketInteraction interaction)
    {

        try
        {

            if (interaction is SocketMessageComponent component)
            {
                var usuario = component.User;

                Console.WriteLine($"Usuário: {usuario.Username}");
                Console.WriteLine($"ID: {usuario.Id}");
            }


            Console.WriteLine($"Handle Interaction arrived! {interaction.Type}");
            var context = new SocketInteractionContext(_client, interaction);

            await _interactions.ExecuteCommandAsync(context, _services);
            Console.WriteLine("Execute command async finished!");



        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


    }

    public async Task<IMessageChannel> GetMessageChannelID(ulong channelID)
    {

        var channel = _client.GetChannel(channelID) as IMessageChannel;

        if (channel == null)
            return null;
        return channel;
    }

       
}

