namespace BookingX.Api.Settings
{
    /// <summary>
    /// ApplicationSettings
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Expected section in the settings file.
        /// </summary>
        public const string Section = "Application";

        /// <summary>
        /// Instructs whether to include or not the Exception Stack Trace in Errors Responses.
        /// </summary>
        public bool IncludeExceptionStackInResponse { get; set; }
    }
}