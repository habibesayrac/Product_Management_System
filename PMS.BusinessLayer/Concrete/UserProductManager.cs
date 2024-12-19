using Newtonsoft.Json;
using PMS.BusinessLayer.Abstract;
using PMS.DataAccessLayer.Abstract;
using PMS.DTOLayer.UserProductDto;
using PMS.EntityLayer;
using PMS.RabbitMq.RabbitMq;
using PMS.Redis.Repository;

namespace PMS.BusinessLayer.Concrete
{
    public class UserProductManager : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public UserProductManager(IUserProductRepository userProductRepository, IRedisRepository redisRepository, IRabbitMqService rabbitMqService)
        {
            _userProductRepository = userProductRepository;
            _redisRepository = redisRepository;
            _rabbitMqService = rabbitMqService;
        }

        public void Add(AddUserProductDto addUserProductDto)
        {
            UserProduct userProduct = new UserProduct()
            {
                UserId = addUserProductDto.UserId,
                ProductId = addUserProductDto.ProductId
            };

            string message = JsonConvert.SerializeObject(userProduct);
            _rabbitMqService.PublishMessage("userproductmesajkuyrugu", message);
       
            _userProductRepository.Add(userProduct);
        }

        public void Delete(int id)
        {
            string message = JsonConvert.SerializeObject(id);
            _rabbitMqService.PublishMessage("userproductmesajkuyrugu", message);
    
            _userProductRepository.Delete(id);
        }

        public UserProduct GetById(int id)
        {
            var cacheResult = _redisRepository.GetData<UserProduct>(id.ToString());
            if (cacheResult == null)
            {
                cacheResult = _userProductRepository.GetById(id);

            }
            _redisRepository.SetData<UserProduct>(id.ToString(), cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }
        public List<UserProduct> GetList()
        {
            var key = "userproductkey";
            var cacheResult = _redisRepository.GetData<List<UserProduct>>(key);
            if (cacheResult == null)
            {
                cacheResult = _userProductRepository.GetList();
            }
            _redisRepository.SetData<List<UserProduct>>(key, cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }
        public bool Update(UpdateUserProductDto updateUserProductDto)
        {
            var updatedUserProduct = GetById(updateUserProductDto.UserId);
            if (updateUserProductDto == null)
            {
                return false;
            }
            string message = JsonConvert.SerializeObject(updatedUserProduct);
            _rabbitMqService.PublishMessage("userproductmesajkuyrugu", message);
          
            return _userProductRepository.Update(updatedUserProduct);
        }
    }
}