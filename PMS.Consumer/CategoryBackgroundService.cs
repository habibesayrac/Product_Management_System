using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PMS.Consumer
{
    public class CategoryBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CategoryBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var categoryConsumer = scope.ServiceProvider.GetRequiredService<ICategoryConsumer>();
                    categoryConsumer.StartConsumingCategories("categorymesajkuyrugu");
                }
                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}