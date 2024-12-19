using Microsoft.Extensions.Logging;
using PMS.DataAccessLayer.Abstract;
using PMS.RabbitMq.RabbitMq;

namespace PMS.Consumer
{
    public class UserProductConsumer : IUserProductConsumer
    {
        private readonly IUserProductRepository _userProductRepository;
        private readonly ILogger<UserProductConsumer> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public UserProductConsumer(IUserProductRepository userProductRepository, ILogger<UserProductConsumer> logger, IRabbitMqService rabbitMqService)
        {
            _userProductRepository = userProductRepository;
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public object StartConsumingUserProducts(string queueName)
        {
            string response = (string)_rabbitMqService.ConsumeMessage(queueName);
            _logger.LogInformation("Userproduct consumer mesaj consume etmeye başladı...");
            return response;
        }
    }
}