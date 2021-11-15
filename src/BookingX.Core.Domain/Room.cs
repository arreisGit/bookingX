using System;
using BookingX.Core.Domain.Exception;

namespace BookingX.Core.Domain
{
    public class Room
    {
        private readonly Guid _id;
        public Guid Id
        {
            get => _id;
            init
            {
                _id = value != Guid.Empty ? value : throw new InvalidEntityIdException(nameof(Id));
            }
        }
    }
}