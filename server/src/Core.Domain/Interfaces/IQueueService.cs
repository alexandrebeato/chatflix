namespace Core.Domain.Interfaces
{
    public interface IQueueService
    {
        Task SendAsync<T>(T message, string queueName);
        Task SubscribeAsync(string queueName, Func<string, Task> OnMessageReceived);
    }
}