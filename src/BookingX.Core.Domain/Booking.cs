using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingX.Core.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }        
        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }
}