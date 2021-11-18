using System;
using System.Threading.Tasks;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using BookingX.Infrastructure.Data.Settings;
using Microsoft.Azure.Cosmos;
namespace BookingX.Infrastructure.Data
{

    // TODO: Use Etag for optimistic cocurrency control
    // TODO: Set a proper CosmosDb partition key and make use of it.
    // In real time scenarios, one would want to make use of a partition key
    // that spreads the records as eavenly possible.
    public class BookingRepository : IBookingRepository
    {
        private readonly Container _container;
        private readonly string _databaseName;
        private readonly string _containerName;
        private readonly string _partitionKey;
        private BookingRepository(
            Container container
        )
        {
            _container = container;
        }

        public static async Task<BookingRepository> Create(
            CosmosClient cosmosClient,
            BookingContainerSettings bookingContainerSettings)
        {
            if (cosmosClient is null)
                throw new ArgumentNullException(nameof(cosmosClient));

            Database database = await GetOrCreateDatabase(
                                        cosmosClient, bookingContainerSettings.Database);

            var container = await GetOrCreateContainer(
                                database,
                                bookingContainerSettings.Container,
                                bookingContainerSettings.PartitionKey);

            return new BookingRepository(container);
        }

        private static async Task<Database> GetOrCreateDatabase(
            CosmosClient cosmosClient,
            string databaseName)
        {
            var databaseResponse = await cosmosClient
                                        .CreateDatabaseIfNotExistsAsync(databaseName);
            return databaseResponse.Database;
        }


        private static async Task<Container> GetOrCreateContainer(
            Database database,
            string container,
            string partitionKey)
        {
            var containerResponse = await database
                                        .CreateContainerIfNotExistsAsync(container, partitionKey);

            return containerResponse.Container;
        }

  
        public async Task<Booking> GetById(Guid id)
        {
            try
            {
                ItemResponse<Booking> response = await _container
                                                .ReadItemAsync<Booking>(
                                                    id.ToString(),
                                                    new PartitionKey(id.ToString()))
                                                .ConfigureAwait(false);
                return response;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // TODO Use an stored procedure to avoid concurrent overlapping reservations.
        public async Task<Booking> CreateBooking(Booking booking)
        {
            ItemResponse<Booking> response = await _container.CreateItemAsync<Booking>(
                                                                booking, 
                                                                new PartitionKey(booking.Id.ToString()),
                                                                new RequestOptions{})
                                                              .ConfigureAwait(false);
            return response;
        }

    }
}