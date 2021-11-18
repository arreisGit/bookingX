using BookingX.Core.Application.Common;
using BookingX.Core.Application.Handlers;
using BookingX.Core.Domain.Interfaces;
using BookingX.Infrastructure.Data.Dummies;
using BookingX.Infrastructure.Data.Settings;
using BookingX.Infrastructure.Data.Stubs;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingX.Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
        
            services.AddSingleton<IRoomRepository,RoomRepositoryStub>();
            services.AddSingleton<IBookingRepository,BookingRepositoryDummy>();
            services.AddMediatR(typeof(GetAllRoomsQueryHandler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            // TODO: App insights.
            CosmosDbSettings cosmosDbSettings = configuration.GetValue<CosmosDbSettings>("ApplicationSettings:InstrumentationKey");

            return services;
        }
    }
}