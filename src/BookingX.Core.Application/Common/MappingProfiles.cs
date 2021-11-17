using AutoMapper;

namespace BookingX.Core.Application.Common
{
     public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Domain.Room, Dtos.Room>();
        }
    }
}