using System;

namespace BookingX.Core.Domain.Exceptions
{
    [Serializable]
    public class BookingValidationException : Exception
    {
        public BookingValidationException() { }

        public BookingValidationException(string message)
            : base(message) { }

        public BookingValidationException(string message, Exception inner)
            : base(message, inner) { }
    }
}