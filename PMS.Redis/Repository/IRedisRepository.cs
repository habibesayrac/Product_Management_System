namespace PMS.Redis.Repository
{
    public interface IRedisRepository
    {
        public T GetData<T>(string key);
        public bool SetData<T>(string key, T value, TimeSpan expirationTime);
    }
}