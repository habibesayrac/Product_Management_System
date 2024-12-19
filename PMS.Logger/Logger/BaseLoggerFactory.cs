namespace PMS.Logger.Logger
{
    public class BaseLoggerFactory : IBaseLoggerFactory
    {
      
        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }
    }
}