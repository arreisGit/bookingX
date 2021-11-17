using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingX.Core.Application.Dtos
{
    public class Room
    {
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [Required]
        public string Number { get; set; }
    }
}