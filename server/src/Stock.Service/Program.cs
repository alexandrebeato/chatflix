
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Infra.CrossCutting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Stock.Service.Interfaces;
using Stock.Service.Services;
using System.Net.Http;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddSingleton<IStooqService, StooqService>();
serviceCollection.AddSingleton<HttpClient>(new HttpClient());
serviceCollection.RegisterServices();
var serviceProvider = serviceCollection.BuildServiceProvider();
var queueService = serviceProvider.GetService<IQueueService>();
var stooqService = serviceProvider.GetService<IStooqService>();

if (queueService == null)
    throw new ArgumentNullException(nameof(queueService));

if (stooqService == null)
    throw new ArgumentNullException(nameof(stooqService));

queueService.SubscribeAsync("stock", async (message) =>
{
    Console.WriteLine($"Received message: {message}");

    var chatCommand = JsonConvert.DeserializeObject<ChatCommand>(message);

    if (chatCommand == null)
        return;

    if (chatCommand.Arguments == null || chatCommand.Arguments.Count == 0)
        return;

    foreach (var stock in chatCommand.Arguments)
        Console.WriteLine(JsonConvert.SerializeObject(await stooqService.GetStocks(stock)));

});

Console.WriteLine("Press [ENTER] to exit.");
Console.ReadLine();