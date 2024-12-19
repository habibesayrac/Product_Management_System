using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMS.Consumer
{
    public class UserBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userConsumer = scope.ServiceProvider.GetRequiredService<IUserConsumer>();
                    userConsumer.StartConsumingUsers("usermesajkuyrugu");
                }
                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}