using BookingX.Core.Application.Common;
using BookingX.Core.Application.Handlers;
using BookingX.Core.Application.Interfaces;
using BookingX.Core.Application.Strategies;
using BookingX.Core.Domain.Interfaces;

using BookingX.Infrastructure.Data;
using BookingX.Infrastructure.Data.Settings;
using BookingX.Infrastructure.Data.Stubs;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookingX.Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddCosmosDbClient(configuration);
            services.AddRepositories();
            services.AddMediatR(typeof(GetAllRoomsQueryHandler).Assembly);
            services.AddSingleton<IRoomsAvailabilitySolverStrategy,RoomsCompleteDaysAvailabilitySolver>();

            //TODO: Validate automapper config
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }

        private static IServiceCollection AddCosmosDbClient(
           this IServiceCollection services,
           IConfiguration configuration)
        {

            var connectionSettings = new CosmosDbConnectionSettings();

            configuration
            .GetSection(CosmosDbConnectionSettings.Section)
            .Bind(connectionSettings);

            var bookingSettingsSection = configuration.GetSection(BookingContainerSettings.Section);
            services.Configure<BookingContainerSettings>(bookingSettingsSection);

            services.AddSingleton<CosmosClient>(sp =>
            {
                return new CosmosClient(
                    connectionSettings.Endpoint,
                    connectionSettings.AuthenticationKey);
            });
            
            return services;
        }

        private static IServiceCollection AddRepositories(
           this IServiceCollection services)
        {

            // Using a Rooms stub to save time with the demostration project.
            services.AddSingleton<IRoomRepository, RoomRepositoryStub>();

            services.AddSingleton<IBookingRepository>( 
                sp =>  {
                     return BookingRepository
                            .Create(
                                sp.GetRequiredService<CosmosClient>(),
                                sp.GetRequiredService<IOptions<BookingContainerSettings>>()
                            )
                            .GetAwaiter()
                            .GetResult();
                });
            return services;
        }
    }
}