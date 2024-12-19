namespace PMS.Logger.Logger
{
    public interface IBaseLoggerFactory
    {
        void Info(string message);
        void Error(string message);
        void Error(Exception exception, string message);
        void Warn(string message);
  
    }
}