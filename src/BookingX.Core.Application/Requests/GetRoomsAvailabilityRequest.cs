using System;
using System.Collections.Generic;
using BookingX.Core.Application.Dtos;
using MediatR;

namespace BookingX.Core.Application.Requests
{
    /// <summary>
    /// GetRoomsAvailability class.
    /// </summary>
    public class GetRoomsAvailabilityRequest : IRequest<IEnumerable<RoomAvailabilityDto>>
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
        /// Initializes a new instance of <see cref="GetRoomsAvailabilityRequest"/> class.
        /// </summary>
        /// <param name="fromDate">The beggining date of the scoped date range.</param>
        /// <param name="toDate">The end date of the scoped date range.</param>
        public GetRoomsAvailabilityRequest(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}