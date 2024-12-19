using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMS.Consumer
{
    public class UserProductBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserProductBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userProductConsumer = scope.ServiceProvider.GetRequiredService<IUserProductConsumer>();
                    userProductConsumer.StartConsumingUserProducts("userproductmesajkuyrugu");

                }
                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}