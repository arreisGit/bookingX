using System;

namespace BookingX.Core.Domain.Exceptions
{
    [Serializable]
    public class InvalidEntityIdException : ArgumentException
    {
        public InvalidEntityIdException(string paramName) : base($"'{paramName}' is not valid.", paramName)
        {
        }
    }
}