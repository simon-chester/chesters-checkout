using ChestersCheckout.Core.Services;
using ChestersCheckout.Core.Services.Abstractions;
using ChestersCheckout.Core.Services.Discounting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChestersCheckout.ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            await host.StartAsync();
            Run(host.Services.CreateScope().ServiceProvider);
            await host.StopAsync();
            await host.WaitForShutdownAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
        {
            services.AddScoped<IProductRepositoryService, StaticProductRepositoryService>();
            services.AddScoped<IEnumerable<IDiscounterService>>(sp => new IDiscounterService[] { 
                new BogofDiscounterService("apple"),
                new ThreeForTwoDiscounterService("orange") 
            });
            services.AddScoped<BasketBuilderService>();
            services.AddScoped<BasketCostCalculatorService>();
        }

        private static void Run(IServiceProvider services)
        {
            Console.WriteLine("Please enter products, seperated by commas");

            var userProducts = Console.ReadLine() ?? "";

            var basketBuilder = services.GetRequiredService<BasketBuilderService>();
            var basket = basketBuilder.BuildBasket(userProducts.Split(',', StringSplitOptions.RemoveEmptyEntries));
            var totalCost = services.GetRequiredService<BasketCostCalculatorService>().CalculateTotalCost(basket);

            Console.WriteLine($"Total cost: {(decimal)totalCost / 100:C}");

            Console.ReadLine();
        }
    }
}
