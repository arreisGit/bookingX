using AutoMapper;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Domain;

namespace BookingX.Core.Application.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<BookingDto, Booking>();
        }
    }
}