namespace BookingX.Infrastructure.Data.Settings
{
    public class CosmosDbConnectionSettings
    {
        public const string Section = "CosmosDb:Connection";
        public string Endpoint { get; set; }
        public string AuthenticationKey { get; set; }
    }
}