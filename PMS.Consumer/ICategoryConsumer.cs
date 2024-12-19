namespace PMS.Consumer
{
    public interface ICategoryConsumer
    {
        public object StartConsumingCategories(string queueName);

    }
}