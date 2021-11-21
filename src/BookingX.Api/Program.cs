using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookingX.Api
{
    // TODO: Add Unit tests (mandatory for handlers!)
    // TODO: Add documentation comments
    // TODO: Implement global exception handler.
    // TODO: Implement logging
    // TODO: Add Validations. Use fluent validation pipeline with Mediatr
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
