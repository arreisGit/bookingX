using System;
using System.Globalization;

namespace BookingX.Core.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStandarizedString(this DateTime date)
        {
            return date.ToString("s", CultureInfo.InvariantCulture);
        }
    }
}