using System;
using System.ComponentModel.DataAnnotations;

namespace BookingX.Core.Application.Dtos
{
    public class BookingDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string RoomId { get; set; }


        //TODO: Decide to go with string or DateTime
        [DataType(DataType.DateTime)]
        public string StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string EndDate { get; set; }

        public string Etag { get; set; }
    }
}