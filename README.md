# CarRentalWCF
This project was created in the form of a submission task in the course "Development with the .NET Framework" at Teknikhögskolan, Gothenburg. This course have been focused on WCF and SOA.

This project implements a WCF service that hosts a car rental system. The WCF service is "self-hosted" through the "CarRentalHost" library, which creates and opens an instance of CarRentalService. The service communicates with two class libraries ("Logic" that further communicate with "Storage"), this is where "business logic" is located and data is saved.

To test the service and its methods, there is a dynamic console client "CarRentalConsoleClient" that can be used. The client speaks with the WCF service with BasicHttpBinding. In addition, there's also an active TCP endpoint, but this is not implemented in the console client. You can also view the data of all saved cars in JSON format with REST through WebHttpBinding.

Where errors might occur, such as when attempting to rent a car that is not available, the service throws FaultExceptions with relevant information.
