using System;
using System.Collections.Generic;
using System.Globalization;
using BookingX.Core.Domain.Shared;

namespace BookingX.Core.Domain.ValueObjects
{
    /// <summary>
    /// DateRange class
    /// </summary>
    public class DateRange : ValueObject
    {
        public DateTime From { get; protected set; }
        public DateTime To { get; protected set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="fromDate">FromDate</param>
        /// <param name="toDate">ToDate</param>
        public DateRange(DateTime fromDate, DateTime toDate)
        {
      
            From = fromDate;
            To = toDate;

            if (fromDate > toDate)
            {
                throw new ArgumentException($"'{nameof(fromDate)}'cannot be greater than {nameof(toDate)}'");
            }
        }

        /// <summary>
        /// PlainText DateRange representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"'{From.ToString("O", CultureInfo.InvariantCulture)}'-'{To.ToString("O", CultureInfo.InvariantCulture)}'";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }
    }
}