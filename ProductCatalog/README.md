# Product Catalog

As part of this assignment, I have created multiple projects.
  - ProductCatalog Web (MVC Front end)
  - ProductCatalog Gateway (Repositories)
  - ProductCatalog API (Service layer)
  - ProductCatalog DataAccess (Data layer)

# Approach!
Database Layer:

  - Implemented EFCore Code-First.
  - I have used .NET Standard project template, which can be referenced either from .NET Core or .NET Framework applications.
  - With .NET Standard project template there wont be any runtime associated. So, I have created all migrations in ProductCatalog API (which is a .NET Core project).
  - When we update the database, it creates a new database in the local sql server instance, and will insert the seed data.
  - Run the below command by setting the ProductCatalog API project as startup project.
```sh
Update-Database
```

Repository Layer:
   - I wanted to create more generic domain objects. So I have chosen Repository pattern to implement it.
   - Implemented ProductCatalog Repository.

Service Layer:
  - Implemented using .NET Core.
  - Used Automapper to map the data from domain object to the model.
  - Configured with Swagger.
  - DB Connection is configured in appsettings.json file.
  - Used .NET Core default ILogger for logging Info, Exceptions.

Front End:
 - Implemented using .NET Core MVC.
 
### Notes/ Improvements
 - I saw 3 approaches for storing product images. 

| Approach | Description |
| ------ | ------ |
| Storing in Database | One of the costly approach |
| Storing to file system | One of the costly approach|
| Storing as a blob in Azure | Easiest way of storing files |

Out of these 3 approaches I have decided to use the first one. Since writing to file system needs to have full access on the folder where it is going to write, and storing to Azure needs subscription and table storage connection strings and all will be exposed in the repo.
