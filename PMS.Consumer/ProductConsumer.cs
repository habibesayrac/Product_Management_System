using Microsoft.Extensions.Logging;
using PMS.DataAccessLayer.Abstract;
using PMS.RabbitMq.RabbitMq;

namespace PMS.Consumer
{
    public class ProductConsumer : IProductConsumer
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductConsumer> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public ProductConsumer(IProductRepository productRepository, ILogger<ProductConsumer> logger, IRabbitMqService rabbitMqService)
        {
            _productRepository = productRepository;
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public object StartConsumingProducts(string queueName)
        {
            string response = (string)_rabbitMqService.ConsumeMessage(queueName);

            _logger.LogInformation("Product consumer mesaj consume etmeye başladı...");

            return response; 
        }
    }
}