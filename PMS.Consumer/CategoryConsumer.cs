using Microsoft.Extensions.Logging;
using PMS.DataAccessLayer.Abstract;
using PMS.RabbitMq.RabbitMq;

namespace PMS.Consumer
{
    public class CategoryConsumer : ICategoryConsumer
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryConsumer> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public CategoryConsumer(ILogger<CategoryConsumer> logger, IRabbitMqService rabbitMqService, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _categoryRepository = categoryRepository;
        }
        public object StartConsumingCategories(string queueName)
        {
            string response = (string)_rabbitMqService.ConsumeMessage(queueName);

            _logger.LogInformation("Category consumer mesaj consume etmeye başladı...");

            return response;
        }
    }
}