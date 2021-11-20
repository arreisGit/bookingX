namespace BookingX.Infrastructure.Data.Settings
{
    public class BookingContainerSettings
    {
         public const string Section = "CosmosDb:Booking";
        public string Database { get; set; }   
        public string Container { get; set; }   
        public string PartitionKey { get; set; }   
    }
}