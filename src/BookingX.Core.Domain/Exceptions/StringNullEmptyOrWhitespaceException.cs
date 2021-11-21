using System;

namespace BookingX.Core.Domain.Exceptions
{
    [Serializable]
    public class StringNullEmptyOrWhitespaceException : SystemException
    {
        public StringNullEmptyOrWhitespaceException(string paramName)
        : base($"'{paramName}' cannot be null, empty or whitespaces only.")
        {
        }
    }
}