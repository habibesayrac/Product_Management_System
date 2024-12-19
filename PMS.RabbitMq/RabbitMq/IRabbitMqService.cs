namespace PMS.RabbitMq.RabbitMq
{
    public interface IRabbitMqService
    {
        void PublishMessage(string queueName, string message);
        string GetMessage(string queueName);
        object ConsumeMessage(string queueName);
    }
}