namespace BookingX.Infrastructure.Data.Settings
{
    public class CosmosDbSettings
    {
        public string Endpoint { get; set; }
        public string AuthKey { get; set; }
        public BookingContainerSettings BookingSettings  { get; set; }
    }
}