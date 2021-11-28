## EFCore

- [whats-new-in-ef-core-6](https://docs.microsoft.com/en-us/events/dotnetconf-2021/whats-new-in-ef-core-6)
- [introducing-devops-friendly-ef-core-migration-bundles](https://devblogs.microsoft.com/dotnet/introducing-devops-friendly-ef-core-migration-bundles/)

EF Core 6 is a cross-platform object-mapper that enables C# developers to use domain classes and strongly typed LINQ queries 
to interface with a backend database. EF Core 6 is the latest version that features performance improvements, 
support for temporal tables, cloud-native-friendly migration bundles, 
improved handling of complex queries and substantial improvements to the Azure Cosmos DB provider. 
See these new features in action and learn when to use them and why they are helpful in this demo-packed session.

https://aka.ms/dotNETConf2021-GetdotNET6


dotnet tool update --global dotnet-ef

dotnet ef
dotnet ef migrations add one


dotnet ef migrations add One

dotnet ef migrations bundle --force

dotnet ef migrations remove


PM> .\efbundle.exe --connection "Server=(localdb)\MSSQLLocalDB;Database=Productions"
