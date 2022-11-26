
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Domain.Messages;
using Domain.Messages.Repository;
using Infra.CrossCutting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using Stock.Service.Interfaces;
using Stock.Service.Services;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IConfiguration>(configuration);

// Mongo database
serviceCollection.AddSingleton<IMongoClient>(mongo => new MongoClient(configuration["mongoConnection:server"]));
BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

serviceCollection.AddSingleton<IStooqService, StooqService>();
serviceCollection.AddSingleton<HttpClient>(new HttpClient());
serviceCollection.RegisterServices();
var serviceProvider = serviceCollection.BuildServiceProvider();
var queueService = serviceProvider.GetService<IQueueService>();
var stooqService = serviceProvider.GetService<IStooqService>();
var messageRepository = serviceProvider.GetService<IMessageRepository>();

if (queueService == null)
    throw new ArgumentNullException(nameof(queueService));

if (stooqService == null)
    throw new ArgumentNullException(nameof(stooqService));

if (messageRepository == null)
    throw new ArgumentNullException(nameof(messageRepository));

queueService.SubscribeAsync("stock", async (message) =>
{
    Console.WriteLine($"Received message: {message}");

    var chatCommand = JsonConvert.DeserializeObject<ChatCommand>(message);

    if (chatCommand == null)
        return;

    if (chatCommand.Arguments == null || chatCommand.Arguments.Count == 0)
        return;

    foreach (var stock in chatCommand.Arguments)
    {
        if (chatCommand.RoomId == null)
            continue;

        var stooqResult = await stooqService.GetStocks(stock);

        if (stooqResult == null || stooqResult.Symbols == null || stooqResult.Symbols.Count() == 0)
            continue;

        Console.WriteLine(JsonConvert.SerializeObject(stooqResult));

        foreach (var symbol in stooqResult.Symbols)
            await messageRepository.Insert(Message.Factory.CreateInstance(Guid.NewGuid(), chatCommand.RoomId.Value, Guid.Empty, "BOT", $"{symbol.Symbol} quote is ${symbol.Close} per share", DateTime.UtcNow));
    }

});

Console.WriteLine("Press [ENTER] to exit.");
Console.ReadLine();