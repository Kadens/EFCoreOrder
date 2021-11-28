// See https://aka.ms/new-console-template for more information
using EFCoreOrder;

Console.WriteLine("Hello, World!");
var timestamps = Seeder.MakeHistory();
Seeder.LookupCurrentPrice("SQLServer");
Seeder.LookupCurrentPrice("Arduino");
Seeder.LokupHistoricPrices("Arduino", timestamps.FirstOrDefault(),timestamps.LastOrDefault());
Seeder.FindOrder("Gun", timestamps.LastOrDefault());
Console.WriteLine(timestamps);