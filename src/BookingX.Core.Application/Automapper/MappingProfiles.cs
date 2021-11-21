using AutoMapper;
using BookingX.Core.Application.Dtos;
using BookingX.Core.Domain.Extensions;
using BookingX.Core.Domain;

namespace BookingX.Core.Application.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<BookingDto, Booking>();

            CreateMap<Booking, BookingDto>()
            .ForMember(
                dest => dest.StartDate,
                opt => opt.MapFrom( src => src.StartDate.ToStandarizedString())
            )
            .ForMember(
                dest => dest.EndDate,
                opt => opt.MapFrom( src => src.EndDate.ToStandarizedString())
            );
                    
        }
    }
}