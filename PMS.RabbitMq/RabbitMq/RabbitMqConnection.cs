using RabbitMQ.Client;

namespace PMS.RabbitMq.RabbitMq
{
    public class RabbitMqConnection : IDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private IModel _channel;

        public RabbitMqConnection()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            try
            {
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
                Console.WriteLine("RabbitMQ bağlantısı kuruldu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitMQ bağlantısı sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }
        public IModel GetChannel()
        {
            if (_channel == null || !_channel.IsOpen)
            {
                _channel = _connection.CreateModel();
            }
            return _channel;
        }
   
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            Console.WriteLine("RabbitMQ bağlantısı kapatıldı.");
        }
    }
}