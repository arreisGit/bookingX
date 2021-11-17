using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookingX.Core.Application.Dtos
{
    public class Room
    {
        [Required]
        [JsonProperty(PropertyName="id")]
        public Guid Id { get; set; }
        
        [Required]
        public string Number { get; set; }
    }
}