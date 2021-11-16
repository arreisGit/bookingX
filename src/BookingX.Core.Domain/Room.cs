using System;
using BookingX.Core.Domain.Exceptions;

namespace BookingX.Core.Domain
{
    public class Room
    {
        public Guid Id { get => _id; }
        public string Number { get => _number; }

        private readonly Guid _id;
        private readonly string _number;

        public Room(Guid id, string number)
        {
            _id = id != Guid.Empty ? id : throw new InvalidEntityIdException(nameof(id));
            _number = !string.IsNullOrWhiteSpace(number)
                        ? number
                        : throw new StringNullEmptyOrWhitespaceException(nameof(number));
        }
    }
}