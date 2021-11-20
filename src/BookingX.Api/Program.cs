using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookingX.Api
{
    // TODO: Add documentation comments
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // TODO: Implement logging
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
