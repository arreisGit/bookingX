using System;

namespace BookingX.Core.Application.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStandardShortString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}