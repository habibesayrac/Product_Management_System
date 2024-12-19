using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMS.Consumer
{
    public class ProductBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productConsumer = scope.ServiceProvider.GetRequiredService<IProductConsumer>();
                    productConsumer.StartConsumingProducts("productmesajkuyrugu");
                }

                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}