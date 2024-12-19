namespace PMS.Consumer
{
    public interface IUserConsumer
    {
        public object StartConsumingUsers(string queueName);

    }
}