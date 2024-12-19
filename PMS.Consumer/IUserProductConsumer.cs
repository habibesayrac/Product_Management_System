namespace PMS.Consumer
{
    public interface IUserProductConsumer
    {
        public object StartConsumingUserProducts(string queueName);
    }
}