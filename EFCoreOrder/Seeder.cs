using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOrder
{
    public class Seeder
    {
        public static List<DateTime> MakeHistory()
        {
            using var context = new OrdersContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var timestamps =new List<DateTime>();

            Console.WriteLine("Making History");

            var customer = new Customer("Gun");
            context.Customers.Add(customer);

            var products = new List<Product>
            { 
                new("SQLServer",10000),
                new("Arduino",80),
                new("RaspberryPI",110)
            
            };

            context.AddRange(products);

            context.SaveChanges();
            timestamps.Add(DateTime.UtcNow);

            Thread.Sleep(500);
            Product product = context.Products.Single(p => p.Name == "Arduino");
            product.Price = 1100;
            products[0].Price = 8100;
            context.SaveChanges();
            timestamps.Add(DateTime.UtcNow);
            Console.WriteLine($"History made!");
            context.Add(new Order(timestamps.Last()) { Customer=customer,Product=products[0]});
            context.SaveChanges();
            timestamps.Add(DateTime.UtcNow);


            return timestamps;
        }
        public static void LookupCurrentPrice(string productName)
        {
            using var context = new OrdersContext();
            var product = context.Products.Single(p=>p.Name == productName);
            Console.WriteLine($"'{productName}' price: {product.Price}");

        }
        public static void FindOrder(string customerName,DateTime on)
        {
            using var context = new OrdersContext();

            var order = context.Orders
                 .TemporalAsOf(on)
                 .Include(e => e.Product)
                .Include(e => e.Customer)
                .Single(order => order.Customer.Name == customerName);
            Console.WriteLine($"{order.Customer.Name} ordered a {order.Product.Name} for {order.Product.Price}");
        }
        public static void LokupHistoricPrices(string productName,DateTime from,DateTime to)
        {
            using var context = new OrdersContext();

            Console.WriteLine($"'{productName}' from: {from}, to:{to}");

            var productSnapshots = context.Products
                //.TemporalAll()
                //.TemporalFromTo(from,to)
                .TemporalAsOf(to)
                .OrderBy(product => EF.Property<DateTime>(product, "PeriodStart"))
                .Where(product => product.Name == productName)
                .Select(product =>
                new
                {
                    Product = product,
                    PeriodStart = EF.Property<DateTime>(product, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(product, "PeriodEnd")
                }).ToList();
            foreach (var productSnapshot in productSnapshots)
            {
                Console.WriteLine($"Product:{productSnapshot.Product.Name}, Price:{productSnapshot.Product.Price}, PeriodStart:{productSnapshot.PeriodStart},PeriodEnd:{productSnapshot.PeriodEnd}");
            }
        }
    }
}
