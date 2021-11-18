using System;
using System.ComponentModel.DataAnnotations;

namespace BookingX.Core.Application.Dtos
{
    public class BookingDto
    {
        [Required]
        public Guid Id {get; set;} 
        [Required]
        public Guid CustomerId {get; set;}
        [Required]
        public Guid RoomId {get; set;}
        [Required]
        public DateTime StartDate {get; set;}
        [Required]
        public DateTime EndDate {get; set;}
    }
}