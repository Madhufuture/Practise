# Accessing On-Premise data using Azure Relay service

Accessing an on-premise system and make data available for the external services like Azure functions, Azure SQL. 
In this application, I'm calling my CacheService.Client application from a Azure Function, and saving the data returned from CacheService.Service to Azure SQL. (But this is currently out of scope for this repo).

## Getting Started

Set CacheService.Service and CacheService.Client as Startup projects.
Set CacheService.Client is dependent on CacheService.Service project.

Note: My preference would be build CacheService.Service project. Run the '.exe' from the '/bin' folder, Just run the CacheService.Client. This way we can ensure that CacheService.Service has already started before the CacheService.Client.

### Prerequisites

1. Get the Relay - namespace and SAS key from the portal.
2. Install ServiceBus nuget package.
```
Install-Package WindowsAzure.ServiceBus -Version 5.1.0
```
3. Replace the Relay namespace and SAS in app.config file in both CacheService.Service and CacheService.Client applications.
