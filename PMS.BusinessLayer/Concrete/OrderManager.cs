using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMS.BusinessLayer.Abstract;
using PMS.DataAccessLayer.Abstract;
using PMS.DTOLayer.OrderDto;
using PMS.EntityLayer;
using PMS.RabbitMq.RabbitMq;
using PMS.Redis.Repository;

namespace PMS.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<OrderManager> _logger;

        public OrderManager(IOrderRepository orderRepository, IRedisRepository redisRepository, IRabbitMqService rabbitMqService, ILogger<OrderManager> logger)
        {
            _orderRepository = orderRepository;
            _redisRepository = redisRepository;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
        }

        public void AddOrder(AddOrderDto addOrderDto)
        {
            string message = JsonConvert.SerializeObject(addOrderDto);
            _rabbitMqService.PublishMessage("ordermesajkuyrugu", message);
            _logger.LogInformation("Order loglanmak üzere alındı.");

            _orderRepository.AddOrder(addOrderDto);
        }

        public void Delete(int id)
        {
            string message = JsonConvert.SerializeObject(id);
            _rabbitMqService.PublishMessage("ordermesajkuyrugu", message);
            _logger.LogInformation("Order silinmek üzere loglandı.");

            _orderRepository.Delete(id);
        }

        public Order GetById(int id)
        {
            var cacheResult = _redisRepository.GetData<Order>(id.ToString());
            if (cacheResult == null)
            {
                cacheResult = _orderRepository.GetById(id);

            }
            _redisRepository.SetData<Order>(id.ToString(), cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }

        public List<Order> GetList()
        {
            var key = "orderkey";
            var cacheResult = _redisRepository.GetData<List<Order>>(key);
            if (cacheResult == null)
            {
                cacheResult = _orderRepository.GetList();
            }
            _redisRepository.SetData<List<Order>>(key, cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }

        public bool Update(UpdateOrderDto updateOrderDto)
        {
            var updatedOrder = GetById(updateOrderDto.OrderId);
            if (updateOrderDto == null)
            {
                return false;
            }

            updateOrderDto.OrderDate = DateTime.Now;

            string message = JsonConvert.SerializeObject(updatedOrder);
            _rabbitMqService.PublishMessage("ordermesajkuyrugu", message);
            _logger.LogInformation("Order güncellenmek üzere alındı.");

            return _orderRepository.Update(updatedOrder);
        }
    }
}