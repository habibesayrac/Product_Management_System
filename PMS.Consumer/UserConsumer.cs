using Microsoft.Extensions.Logging;
using PMS.DataAccessLayer.Abstract;
using PMS.RabbitMq.RabbitMq;

namespace PMS.Consumer
{
    public class UserConsumer : IUserConsumer
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserConsumer> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public UserConsumer(IUserRepository userRepository, ILogger<UserConsumer> logger, IRabbitMqService rabbitMqService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public object StartConsumingUsers(string queueName)
        {
            string response = (string)_rabbitMqService.ConsumeMessage(queueName);

            _logger.LogInformation("User consumer mesaj consume etmeye başladı...");

            return response;
        }
    }
}