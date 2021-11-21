using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookingX.Api
{
    // TODO: Add Validations. Use fluent validation pipeline with Mediatr
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
