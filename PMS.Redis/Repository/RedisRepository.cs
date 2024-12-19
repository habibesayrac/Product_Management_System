using Newtonsoft.Json;
using StackExchange.Redis;

namespace PMS.Redis.Repository
{
    public class RedisRepository : IRedisRepository
    {
        private IDatabase _db;

        public RedisRepository()
        {
            var connection = ConnectionMultiplexer.Connect("localhost");
            _db = connection.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);

            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public bool SetData<T>(string key, T value, TimeSpan expirationTime)
        {
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expirationTime);

            return isSet;
        }
    }
}