# Blog
Clean Architecture for Web API + Angular SPA.

## Create Database & apply migrations
You must update your connection string in the ConnectionStrings/DefaultConnection section on the appsettings.json file included in WebUI project.

ex: "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=DB_NAME;Integrated Security=True"
  }
  
  
  After that, open a CMD window to run the following command:
  
  
```powershell
dotnet ef database update --project src\Infrastructure --startup-project src\WebUI
```

## Running Application
locate in ~/src/WebUI/ and open an CMD window to type the follwing command:

```powershell
dotnet run
```
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
