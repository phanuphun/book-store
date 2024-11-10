## Book Store Management System
- [x] Register / Login
- [x] Create Delete Update Account User
- [x] Create Delete Update Book Category
- [x] Create Delete Update Book
- [ ] Bookmark
- [ ] Cart
- [ ] Order
- [ ] History

## Installation and Setup
1. Run `dotnet --version` to makesure you have aleady installed .NET CORE SDK , If not install first.
2. Run `dotnet restore` to install packages (Entity Framework Core :EF Core).
3. Makesure you have aready install SQL Server and SSMS(Optional).
4. Configure  `ConnectionString` for SQL Server in `./appsetting.json` 
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=<DatabaseName>;Database=BookStore;Trusted_Connection=True;TrustServerCertificate=True;"
  }
```
4. Run `dotnet ef database update` to migrate database to SQL Server Or if you use NuGet Console, you can run `update-database` instead.
5. Run `dotnet run` to start project on locallhost.
