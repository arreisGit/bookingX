using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookingX.Api
{
    // TODO: Add UseCases unit tests
    // TODO: Add documentation comments
    // TODO: Get rid of CQRS, is not useful for this simple case.
    // TODO: Implement global exception handler.
    // TODO: App insights for logging and telemetry.
    // TODO: Implement logging
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

       
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
