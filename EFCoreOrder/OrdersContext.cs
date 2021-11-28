using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOrder
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=Productions;Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable(tableBuilder => tableBuilder.IsTemporal());
            modelBuilder.Entity<Product>()
                .ToTable(tableBuilder => tableBuilder.IsTemporal());
            modelBuilder.Entity<Order>()
                .ToTable(tableBuilder => tableBuilder.IsTemporal());

        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(200);
        }
        }
}
