using System;


namespace BookingX.Api.Models
{

    /// <summary>
    /// ExceptionResponse
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Inner Exception StackTrace
        /// </summary>
        public string InnerException { get; set; }
    }
}