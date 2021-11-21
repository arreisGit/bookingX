using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingX.Core.Domain;
using BookingX.Core.Domain.Interfaces;
using BookingX.Core.Domain.ValueObjects;
using BookingX.Infrastructure.Data.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace BookingX.Infrastructure.Data
{
    // TODO: Use Etag for optimistic cocurrency control.
    // TODO: Implement Base Repository for reusability

    /// <summary>
    /// BookingRepository class
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        private readonly Container _container;
        private static string DateTimeToShortStringFormat = "yyyy-MM-dd";

        private BookingRepository(
            Container container
        )
        {
            _container = container;
        }

        /// <summary>
        /// Instantiates <see cref="BookingRepository"/> object.
        /// </summary>
        /// <param name="cosmosClient">CosmosClient</param>
        /// <param name="bookingContainerSettings">Booking container settings</param>
        /// <returns>A BookingRepository instance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<BookingRepository> Instantiate(
            CosmosClient cosmosClient,
            IOptions<BookingContainerSettings> bookingContainerSettings)
        {
            if (cosmosClient is null)
                throw new ArgumentNullException(nameof(cosmosClient));

            if (bookingContainerSettings?.Value is null)
                throw new ArgumentNullException(nameof(bookingContainerSettings));

            var settings = bookingContainerSettings.Value;

            Database database = await GetOrCreateDatabase(cosmosClient, settings.Database);

            var container = await GetOrCreateContainer(
                                database,
                                settings.Container,
                                settings.PartitionKey);

            return new BookingRepository(container);
        }

        /// <summary>
        /// Ensures the Database is created in the CosmosDb account and returns it
        /// </summary>
        /// <param name="cosmosClient">CosmosClient</param>
        /// <param name="databaseName">Database Name</param>
        /// <returns>CosmosDB Database</returns>
        private static async Task<Database> GetOrCreateDatabase(
            CosmosClient cosmosClient,
            string databaseName)
        {
            var databaseResponse = await cosmosClient
                                        .CreateDatabaseIfNotExistsAsync(databaseName);
            return databaseResponse.Database;
        }


        /// <summary>
        /// Ensures the container is created in the CosmosDb account and returns it
        /// </summary>
        /// <param name="database">CosmosDb Database</param>
        /// <param name="containerName">Container Name</param>
        /// <param name="partitionKey">The partitionKey to be set when creating the container</param>
        /// <returns>A CosmosDb Container</returns>
        private static async Task<Container> GetOrCreateContainer(
            Database database,
            string containerName,
            string partitionKey)
        {
            var containerResponse = await database
                                        .CreateContainerIfNotExistsAsync(containerName, partitionKey);

            return containerResponse.Container;
        }

        /// <summary>
        /// Gets the Booking by Id
        /// </summary>
        /// <param name="id">The Booking Id</param>
        /// <returns>Booking</returns>
        public async Task<Booking> GetByIdAsync(Guid id)
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

        // TODO Use an stored procedure to avoid concurrent overlapping reservations
        /// <summary>
        /// Creates a Booking
        /// </summary>
        /// <param name="booking">The Booking to be created</param>
        /// <returns>The resulting Booking</returns>
        public async Task<Booking> CreateAsync(Booking booking)
        {
            ItemResponse<Booking> response = await _container.CreateItemAsync<Booking>(
                                                                booking,
                                                                new PartitionKey(booking.Id.ToString()))
                                                              .ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Updates a booking
        /// </summary>
        /// <param name="booking">The Booking to be updated</param>
        /// <returns>True if the update process succeded, false otherwhise.</returns>
        public async Task<bool> UpdateAsync(Booking booking)
        {
            try
            {
                ItemResponse<Booking> response = await _container
                                                .UpsertItemAsync<Booking>(
                                                    booking,
                                                    new PartitionKey(booking.Id.ToString())
                                                ).ConfigureAwait(false);
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound || ex.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a booking.
        /// </summary>
        /// <param name="id">The Id that corresponds to the booking to be deleted.</param>
        /// <returns>True if the deletion succeded, false otherwhise.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                ItemResponse<Booking> response = await _container
                                                        .DeleteItemAsync<Booking>(
                                                            id.ToString()
                                                            , new PartitionKey(id.ToString())
                                                        ).ConfigureAwait(false);
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        /// <summary>
        /// Searches for all existent bookings between a date range and returns them
        /// </summary>
        /// <param name="dateRange">The date range that will be used to look for bookings</param>
        /// <returns>A collection of all the bookings that have either the start or end date inside the date range</returns>
        public async Task<IEnumerable<Booking>> GetAllBookingsInDateRange(DateRange dateRange)
        {
            string query = GetBookingsBetweenDatesQuery(dateRange);

            var iterator = _container.GetItemQueryIterator<Booking>(new QueryDefinition(query));
            List<Booking> results = new List<Booking>();

            while (iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync().ConfigureAwait(false);

                results.AddRange(result.ToList());
            }

            return results;
        }


        /// <summary>
        /// Gets the SQL API query to search for bookings inside the date range
        /// </summary>
        /// <param name="dateRange">The date range that will be used to search for bookings</param>
        /// <returns>SQL API query.</returns>
        private static string GetBookingsBetweenDatesQuery(DateRange dateRange)
        {
            string fromDate = dateRange.From.ToString(DateTimeToShortStringFormat);
            string toDate = dateRange.To.ToString(DateTimeToShortStringFormat);

            var query = new StringBuilder();
            query.Append("SELECT * FROM b ");
            query.Append("WHERE ");
            query.Append($"(b.StartDate BETWEEN '{fromDate}' AND '{toDate}')");
            query.Append($" OR (b.EndDate BETWEEN '{fromDate}' AND '{toDate}')");

            return query.ToString();
        }
    }
}