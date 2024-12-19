using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMS.Consumer
{
    public class OrderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var orderConsumer = scope.ServiceProvider.GetRequiredService<IOrderConsumer>();
                    orderConsumer.StartConsumingOrders("ordermesajkuyrugu");
                }

                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}