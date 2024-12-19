using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMS.BusinessLayer.Abstract;
using PMS.DataAccessLayer.Abstract;
using PMS.DTOLayer.UserDto;
using PMS.EntityLayer;
using PMS.RabbitMq.RabbitMq;
using PMS.Redis.Repository;

namespace PMS.BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<UserManager> _logger;

        public UserManager(IUserRepository userRepository, IRedisRepository redisRepository, IRabbitMqService rabbitMqService, ILogger<UserManager> logger)
        {
            _userRepository = userRepository;
            _redisRepository = redisRepository;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
        }

        public void Add(AddUserDto addUserDto)
        {
            User user = new User()
            {
                Name = addUserDto.Name
            };

            string message = JsonConvert.SerializeObject(user);
            _rabbitMqService.PublishMessage("usermesajkuyrugu", message);
            _logger.LogInformation("User loglanmak üzere alındı.");

            _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            string message = JsonConvert.SerializeObject(id);
            _rabbitMqService.PublishMessage("usermesajkuyrugu", message);
            _logger.LogInformation("User silinmek için loglandı.");

            _userRepository.Delete(id);
        }

        public User GetById(int id)
        {
            var cacheResult = _redisRepository.GetData<User>(id.ToString());
            if (cacheResult == null)
            {
                cacheResult = _userRepository.GetById(id);
            }
            _redisRepository.SetData<User>(id.ToString(), cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;

        }
        public List<User> GetList()
        {
            var key = "userkey";
            var cacheResult = _redisRepository.GetData<List<User>>(key);
            if (cacheResult == null)
            {
                cacheResult = _userRepository.GetList();
            }
            _redisRepository.SetData<List<User>>(key, cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }

        public bool Update(UpdateUserDto updateUserDto)
        {
            var updatedUser = GetById(updateUserDto.UserId);
            if (updatedUser == null)
            {
                return false;
            }
            updatedUser.Name = updateUserDto.Name;

            string message = JsonConvert.SerializeObject(updatedUser);
            _rabbitMqService.PublishMessage("usermesajkuyrugu", message);
            _logger.LogInformation("User güncellenmek üzere loglandı.");

            return _userRepository.Update(updatedUser);
        }
    }
}