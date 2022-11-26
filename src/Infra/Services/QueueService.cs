using System.Text;
using Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infra.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConnectionFactory _connectionFactory;

        public QueueService(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var connectionUri = configuration["rabbitmq:uri"];

            if (string.IsNullOrEmpty(connectionUri))
                throw new ArgumentException("RabbitMQ connection URI is not set in configuration.", nameof(configuration));

            Console.WriteLine($"RabbitMQ connection URI: {connectionUri}");

            _connectionFactory = new ConnectionFactory() { Uri = new Uri(connectionUri) };
        }

        public Task SendAsync<T>(T message, string queueName)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            return Task.CompletedTask;
        }

        public Task SubscribeAsync(string queueName, Func<string, Task> OnMessageReceived)
        {
            var channel = _connectionFactory.CreateConnection().CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                OnMessageReceived(message);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}