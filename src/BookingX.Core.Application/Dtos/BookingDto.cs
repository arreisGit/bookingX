using System;
using System.ComponentModel.DataAnnotations;

namespace BookingX.Core.Application.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value.Date;
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value
                          .Date
                          .AddHours(23)
                          .AddMinutes(59)
                          .AddSeconds(59)
                          .AddMilliseconds(999);
            }
        }

        private DateTime _startDate;
        private DateTime _endDate;
    }
}