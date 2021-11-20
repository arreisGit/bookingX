# BookingX
## Alten demostration project.

This project represents a Booking API that allows the user to do the following:
* Get a list of rooms.  
* Get a list of availability date ranges by room. 
* Book a room.  
* See the details of a booking.  
* Edit a booking  
* Delete a booking. 

## Created With
To be able to comply the 99.99% uptime SLI, the following decisions were made.

#### Technologies
* .NET Core 5 will be used to create  the REST API
* CosmosDb will be used for the backend storage

#### Planned Services/Infrastructure for the service high availability (99.99%)
* Azure App Service to host the REST API 
* Azure Application Insights for logging and telemetry
* Azure CosmosDb 
* Azure Vault for Secrets storage
* Azure traffic Manager for load balancing.
 
## Time Saving Shortcuts
To save some developing time on this project, some notable shortcuts where taken, which would have to be handled for real life scenarios.

* The booking id was used for the CosmsosDb partition Key. On a real life case, the partition key needs to be carefully chosen.
* NO distributed lock for Booking Creation. Due the nature of this service and its backend store, some concurrency control mechanism would have to be implemented.
* NO user authentication nor authorization
* NO CORS policy
* NO Caching
* NO custom Swashbuckle examples where created.
* Not all the classes have XML comments documentation.
* An stubbed Repostitory for the rooms is being used.