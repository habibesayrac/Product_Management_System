using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PMS.RabbitMq.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConnection _connection;

        public RabbitMqService(RabbitMqConnection connection)
        {
            _connection = connection;
        }
        public string GetMessage(string queueName)
        {
            try
            {
                var channel = _connection.GetChannel();

                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var result = channel.BasicGet(queue: queueName, autoAck: true);

                if (result == null)
                {
                    Console.WriteLine("Kuyrukta mesaj bulunamadı.");
                    return string.Empty;
                }

                var message = Encoding.UTF8.GetString(result.Body.ToArray());
                Console.WriteLine($"Kuyruktan mesaj alındı: {message}");
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mesaj alma sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }
        public void PublishMessage(string queueName, string message)
        {
            try
            {
                var channel = _connection.GetChannel();

                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

                Console.WriteLine($"Mesaj kuyruğa gönderildi: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mesaj gönderiminde hata oluştu: {ex.Message}");
                throw;
            }
        }
        public object ConsumeMessage(string queueName) // void metot object olarak değiştirildi
        {
            var channel = _connection.GetChannel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Mesaj alındı: {message}");

            };
            channel.BasicConsume(queue: queueName,
                            autoAck: true,
                            consumer: consumer);

            Console.WriteLine("Kuyrukta mesajlar bekleniyor...");

            return "Ok";
        }
    }
}