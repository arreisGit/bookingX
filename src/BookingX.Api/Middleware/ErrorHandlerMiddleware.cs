using System;
using System.Threading.Tasks;
using BookingX.Api.Models;
using BookingX.Api.Settings;
using BookingX.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BookingX.Api.Middleware
{
    /// <summary>
    /// Central error/exception handler Middleware
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private const string JsonContentType = "application/json";

        /// <summary>
        /// Reference to the next middleware.
        /// </summary>
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// Instance of the logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Indicates if the responses should contain exception details.
        /// </summary>
        private readonly bool _includeExceptionStackInResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="requestDelegate">Next middleware.</param>
        /// <param name="logger">Logger instance.</param>
        /// <param name="applicationSettings">Options accessor.</param>
        public ErrorHandlerMiddleware(
            RequestDelegate requestDelegate,
            ILogger<ErrorHandlerMiddleware> logger,
            IOptions<ApplicationSettings> applicationSettings)
        {
            if (applicationSettings == null)
                throw new ArgumentNullException(nameof(applicationSettings));

            _requestDelegate = requestDelegate;
            _logger = logger;
            _includeExceptionStackInResponse = applicationSettings.Value.IncludeExceptionStackInResponse;
        }

        /// <summary>
        /// Method is invoked when middleware is switched.
        /// </summary>
        /// <param name="context">Context of the invocation.</param>
        /// <returns>Task of the invocation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (_requestDelegate != null)
                    await _requestDelegate(context).ConfigureAwait(false);
            }
            catch (FluentValidation.ValidationException ex)
            {
                IEnumerable<string> errorMessages = ex
                                            .Errors
                                            .Select(err => $"{err.PropertyName}: {err.ErrorMessage}");

                var errorResponse = new ErrorResponse
                {
                    Errors = errorMessages,
                    InnerException = _includeExceptionStackInResponse ?
                                $"{ex.GetType().FullName}: {ex.Message} {ex.StackTrace}"
                                : null
                };

                await WriteResponse(context, errorResponse, StatusCodes.Status400BadRequest)
                .ConfigureAwait(false);
            }
            catch (BookingValidationException ex)
            {
                var errorResponse = ExceptionToErrorResponse(ex);
                await WriteResponse(context, errorResponse, StatusCodes.Status400BadRequest)
                   .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionToErrorResponse(ex);
                _logger.LogCritical(ex, "Exception captured in error handling middleware");

                await WriteResponse(
                    context,
                    errorResponse,
                    StatusCodes.Status500InternalServerError)
                .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Converts an Exception into an ErrorResponse object
        /// </summary>
        /// <param name="exception">The Exception to be converted</param>
        /// <returns>An ErrorResponse</returns>
        private ErrorResponse ExceptionToErrorResponse(Exception exception)
        {
            return new ErrorResponse
            {
                Errors = new string[] { exception.Message },
                InnerException = _includeExceptionStackInResponse ?
                              $"{exception.GetType().FullName}: {exception.Message} {exception.StackTrace}"
                              : null
            };
        }

        /// <summary>
        /// Writes the response to the current context.
        /// </summary>
        /// <param name="context">Context of the invocation</param>
        /// <param name="errorResponse">The exception that got raised</param>
        /// <param name="httpStatusCode">The HttpStatusCode that will be written used for the response</param>
        /// <returns>Task of invocation</returns>
        private async Task WriteResponse(HttpContext context, ErrorResponse errorResponse, int httpStatusCode)
        {
            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = JsonContentType;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}