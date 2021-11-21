using System;
using System.Collections.Generic;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Queries
{
    /// <summary>
    /// GetRoomsAvailability class.
    /// </summary>
    public class GetRoomsAvailabilityQuery : IRequest<IEnumerable<RoomAvailabilityDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime FromDate { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime ToDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GetRoomsAvailabilityQuery"/> class.
        /// </summary>
        /// <param name="fromDate">The beggining date of the scoped date range.</param>
        /// <param name="toDate">The end date of the scoped date range.</param>
        public GetRoomsAvailabilityQuery(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}