using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Models;
using Bogus;

namespace OrderManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Key for OrderDetail
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });

            // Seed Data
            var users = new List<User>
            {
                new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
            };

            var products = new List<Product>();
            for (int i = 1; i <= 30; i++)
            {
                products.Add(new Product { Id = i, Name = $"Product {i}", Price = 10 + i });
            }

            var orders = new List<Order>();
            for (int i = 1; i <= 10; i++)
            {
                orders.Add(new Order { Id = i, OrderDate = DateTime.Now.AddDays(-i), UserId = (i % 2) + 1 });
            }

            var orderDetails = new List<OrderDetail>();
            int detailId = 1;

            for (int i = 1; i <= 10; i++) // Loop through Orders
            {
                for (int j = 1; j <= 3; j++) // Add 2-3 products to each Order
                {
                    orderDetails.Add(new OrderDetail
                    {
                        OrderId = i,
                        ProductId = ((i + j) % 30) + 1, // Ensures unique ProductId
                        Quantity = j,
                        UnitPrice = 10 + ((i + j) % 30)
                    });
                    detailId++;
                }
            }

            // Apply Seed Data
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<OrderDetail>().HasData(orderDetails);
        }

    }
}
