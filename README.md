# BookingX
## Alten demostration project.

This project represents a Booking API that allows the user to do the following:
* Get a list of rooms.
* Get a list of roomas available between two dates.
* Book a room.  
* Get the details of a booking.  
* Update a booking.
* Delete a booking. 

## Created With
To be able to comply the 99.99% uptime SLI, the following decisions were made.

#### Technologies
* .NET Core 5 will be used to create  the REST API.
* CosmosDb will be used for the backend storage.

#### Planned Services/Infrastructure for the service high availability (99.99%)
* Azure App Service to host the REST API.
* Azure Application Insights for logging and telemetry.
* Azure CosmosDb.
* Azure Vault for Secrets storage.
* Azure traffic Manager for load balancing.
 
## Time Saving Shortcuts
To save some developing time on this project, some notable shortcuts where taken which would have to be handled for real life scenarios.

* For the booking creation, two calls are being made to Cosmosdb, the first one checks if there any overlapping bookings for the room and the second creates it. In real life-scenarios, an stored procedured would have been used instead.
* The booking id was used for the CosmsosDb partition Key. On a real life case, the partition key needs to be carefully chosen.
* NO distributed lock for Booking Creation. Due the nature of this service and its backend store, some concurrency control mechanism would have to be implemented.
* NO user authentication nor authorization.
* NO CORS policy.
* NO Caching.
* NO custom Swashbuckle examples nor the usage of xml comments for documentation were setup.
* Not all the classes have XML comments documentation.
* An stubbed Repostitory for the rooms is being used.
* NO BaseRepository implementation.
* NO usage of CorrelationId that in real-life situations help with traceability.
* Domain Booking Entity has two NewtonSoft attributes to map Id and ETag to the corresponding CosmosDB properties. I'm aware that this is violating the Clean Architecture layer encapsulation, as the Domain should not know nor care about the Infrastructure Data mechanisms. Some of the possible solutions are to set up the EF Cosmos provider and configure all the necessary mappings there, implement a Booking Data DTO to use it for CosmosDb communication and map it over to the Domain Object, or use a custom deserializer in cosmos client.

## Usage
Running the application is quite simple, you just to have installed .NET 5 SDK and an access to any CosmosDb server, including CosmosDb local emulator ([Install and use the Azure Cosmos DB Emulator for local development and testing](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21)).

It is not recommended to set up secrets in the appsettings.json file, but for the sake of a simple test you can add your CosmosDb settings directly in it, or if you prefer you can set them up as User Secrets ([Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)).

Once you have the CosmosDb connection setup, you can launch the BookingX.Api by with Visual Studio, Visual Studio Code, or the dotnet CLI.


A 'BookingX.postman_collection.json' Postman collection file has been included for convinience in the root folder, you can import it or open [https:localhost:5001/swagger](https:localhost:5001/swagger) to easily test the available endpoints, which are:


**Bookings**
 -  GET /bookings/{id} | Gets the detail for the specified Booking.
 -  POST /bookings     | Creates a new Booking.
 -  PUT /bookings/{id} | Updates a Booking.
 -  DELETE /bookings/{id} | Gets the detail for the specified Booking.

**Rooms**
 - GET /rooms | Gets a list of all the existen rooms.
 - GET /rooms/availability?fromDate={fromDate}&toDate={toDate} | Gets a list of all the available rooms between a date range.