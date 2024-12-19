using Microsoft.Extensions.Logging;
using PMS.DataAccessLayer.Abstract;
using PMS.RabbitMq.RabbitMq;

namespace PMS.Consumer
{
    public class OrderConsumer : IOrderConsumer
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderConsumer> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public OrderConsumer(IOrderRepository orderRepository, ILogger<OrderConsumer> logger, IRabbitMqService rabbitMqService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }
        public object StartConsumingOrders(string queueName)
        {
            string response = (string)_rabbitMqService.ConsumeMessage(queueName);

            _logger.LogInformation("Order consumer mesaj consume etmeye başladı...");

            return response;
        }
    }
}