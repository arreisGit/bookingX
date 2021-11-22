using System;
using System.Collections.Generic;

namespace BookingX.Api.Models
{
    /// <summary>
    /// ExceptionResponse
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Errors
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Inner Exception StackTrace
        /// </summary>
        public string InnerException { get; set; }
    }
}