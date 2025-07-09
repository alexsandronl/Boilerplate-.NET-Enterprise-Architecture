namespace Infrastructure.ServiceBus
{
    /// <summary>
    /// Abstração para publicação e consumo de mensagens no RabbitMQ.
    /// </summary>
    public interface IRabbitMqService
    {
        void Publish(string queue, string message);
        void Subscribe(string queue, Action<string> onMessage);
    }
} 