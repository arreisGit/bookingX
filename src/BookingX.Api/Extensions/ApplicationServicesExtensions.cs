using BookingX.Core.Application.Common;
using BookingX.Core.Application.Handlers;
using BookingX.Core.Domain.Interfaces;
using BookingX.Infrastructure.Data.Stubs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookingX.Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
        
            services.AddSingleton<IRoomRepository,RoomRepositoryStub>();
            services.AddMediatR(typeof(GetAllRoomsQueryHandler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}