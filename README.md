# Blog
Clean Architecture for Web API + Angular SPA.

## Prerequisites

Dotnet 6 SDK
SQL Server
IDE-Visual Studio(optional)


## Create Database & apply migrations

You must verify that the DefaultConnection connection string within appsettings.json located in WebUI project points to a valid SQL Server instance.

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

ex: "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=DB_NAME;Integrated Security=True"
  }
  
  
  After that, open a CMD window to run the following commands to ensure dotnet tools is updated with the latest version to avoid issues creating the DB:
  
  
```powershell
dotnet tool update --global dotnet-ef

dotnet ef database update --project src\Infrastructure --startup-project src\WebUI
```

## Running Application
locate in ~/src/WebUI/ and open an CMD window to type the follwing command:

```powershell
dotnet run
```
Or simply open your IDE, I recommend Visual Studio, select WebUI project as startup project and run the project.

## Open app in web browser
Open url https://localhost:5000 in your prefered web browser and wait until the app start.
the app will redirect you to a final url handled by proxy.

## Sample data to Login.

```
Username:publicuser@localhost

Password:Pa$$word1!

Role: Public
```
```
Username:writeruser@localhost

Password:Pa$$word1!

Role: Writer
```
```
Username:writeruser2@localhost

Password:Pa$$word1!

Role: Writer
```
```
Username:editoruser@localhost

Password:Pa$$word1!

Role: Editor
```
```
Username:administrator@localhost

Password:Administrator1!

Role: Administrator
```

## A Total amount of 32 hours were necessary to build this app.
