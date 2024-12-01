# CafePortalAPI
Prerequisites
  General Requirements
  Software:
    .NET 8.x or above
     Visual Studio (or any compatible IDE for .NET Core)
 Database:
    One of the following RDBMS:
      MSSQL
      PostgreSQL
      MySQL
      
1. Clone the Repository
git clone <repository-url>
cd <repository-folder>
run dotnet restore

3. Configure Environment Variables
   1. Create a file named appsettings.json in the backend project folder if it doesn’t already exist.
   2. Update the connection string and settings:
      {
      "ConnectionStrings": {
        "DefaultConnection": "Server=<server>;Database=<database>;User Id=<user>;Password=<password>;"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      }
    }

4. Initialize the Database
Use a database client to execute the DB_schema.sql file located in the DbSchema folder to create tables and seed data:

5. Run the Backend
dotnet run

The backend will be available at http://localhost:5114 by default.
