using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestoranClient.Data
{
    public class RestoranDbContext : DbContext
    {
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Abonent> Abonent { get; set; }
        public DbSet<SourceItem> Sources { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Detail> Details { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
         optionsBuilder.UseSqlServer(Config.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-8U4GJ5C\\MSSQLSERVER01;Initial Catalog=RestorantNewNew;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Waiter>(entity =>
            {
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Name).HasColumnName("name");
                entity.Property(p => p.Password).HasColumnName("password");
                entity.HasData(new Waiter { Id = 1, Name = "Маріка", Password = "11112222" },
                               new Waiter { Id = 2, Name = "Катерина", Password = "22222222" },
                               new Waiter { Id = 3, Name = "Жужа", Password = "33332222" },
                               new Waiter { Id = 4, Name = "Лєнка", Password = "44442222" },
                               new Waiter { Id = 5, Name = "Валєра", Password = "55552222" },
                               new Waiter { Id = 6, Name = "Валентина", Password = "66662222" },
                               new Waiter { Id = 7, Name = "Олександр", Password = "77772222" },
                               new Waiter { Id = 8, Name = "Степан", Password = "88882222" }
);
            });

            modelBuilder.Entity<Abonent>(entity =>
            {
                entity.ToTable("abonent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasData(new Abonent { Id = 1, Name = "table 1" },
                               new Abonent { Id = 2, Name = "table 2" },
                               new Abonent { Id = 3, Name = "table 3" },
                               new Abonent { Id = 4, Name = "table 4" },
                               new Abonent { Id = 5, Name = "table 5" },
                               new Abonent { Id = 6, Name = "table 6" },
                               new Abonent { Id = 7, Name = "table 7" },
                               new Abonent { Id = 8, Name = "table 8" });

            });
            modelBuilder.Entity<ClientCards>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.ToTable("items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 2)");
                entity.HasData(
                    new FoodItem { Name = "Bear", Price = 45.00M, Id = 1 },
                    new FoodItem { Name = "Borch", Price = 50.00M, Id = 2 },
                    new FoodItem { Name = "bread", Price = 5.00M, Id = 3 },
                    new FoodItem { Name = "Chicken soup", Price = 45.00M, Id = 4 },
                    new FoodItem { Name = "chicken with poatoes", Price = 95.00M, Id = 5 },
                    new FoodItem { Name = "Coca Cola", Price = 40.00M, Id = 6 },
                    new FoodItem { Name = "Cofee", Price = 25.00M, Id = 7 },
                    new FoodItem { Name = "Duck soup", Price = 45.00M, Id = 8 },
                    new FoodItem { Name = "Marenga", Price = 243.00M, Id = 18 },
                    new FoodItem { Name = "White coffe", Price = 56.00M, Id = 20 },
                    new FoodItem { Name = "IceCream", Price = 50.00M, Id = 9 },
                    new FoodItem { Name = "Ketchup", Price = 300.00M, Id = 10 },
                    new FoodItem { Name = "Pizza", Price = 95.00M, Id = 11 },
                    new FoodItem { Name = "Russian salat", Price = 34.00M, Id = 17 },
                    new FoodItem { Name = "Salad", Price = 100.00M, Id = 12 },
                    new FoodItem { Name = "Shashlik", Price = 300.00M, Id = 13 },
                    new FoodItem { Name = "Teckila", Price = 56.00M, Id = 21 },
                    new FoodItem { Name = "Vodka", Price = 300.00M, Id = 14 },
                    new FoodItem { Name = "Water", Price = 40.00M, Id = 15 },
                    new FoodItem { Name = "Whiskey", Price = 1000.00M, Id = 16 }
                    );

            });

            var sourceConverter = new ValueConverter<FixSource, string>(
                    v => v.ToString(),
                    v => (FixSource)Enum.Parse(typeof(FixSource), v));
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(p => p.WaiterId).HasColumnName("waiter_id");
                entity.Property(p => p.AbonentId).HasColumnName("abonent_id");
                entity.Property(p => p.SourceId).HasColumnName("source_id");
                entity.Property(p => p.TimeOrder).HasColumnName("time_order");
                entity.Property(p => p.EndOrder).HasColumnName("end_order");


                entity.Property(p => p.FixedSource).HasConversion(sourceConverter);
            });
            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(p => p.OrderId).HasColumnName("order_id");
                entity.Property(p => p.ItemsId).HasColumnName("items_id");
                entity.Property(p => p.Bill).HasColumnName("bill");
                entity.Property(p => p.Count).HasColumnName("count");
                entity.Property(p => p.Price).HasColumnName("price");

                entity.HasData(
                new Detail { Bill = 10.000M, Count = 2.000M, Price = 50.00M, Id = 1, ItemsId = 2, OrderId = 1 },
                new Detail { Bill = 90.00M, Count = 2.000M, Price = 45.00M, Id = 2, ItemsId = 8, OrderId = 2 },
                new Detail { Bill = 150.00M, Count = 0.500M, Price = 300.00M, Id = 3, ItemsId = 14, OrderId = 3 },
                new Detail { Bill = 45.00M, Count = 1.000M, Price = 45.00M, Id = 4, ItemsId = 4, OrderId = 4 },
                new Detail { Bill = 50.00M, Count = 2.000M, Price = 25.00M, Id = 5, ItemsId = 7, OrderId = 5 },
                new Detail { Bill = 75.00M, Count = 3.000M, Price = 25.00M, Id = 6, ItemsId = 7, OrderId = 5 },
                new Detail { Bill = 1125.00M, Count = 25.000M, Price = 45.00M, Id = 7, ItemsId = 4, OrderId = 10 },
                new Detail { Bill = 1125.00M, Count = 25.000M, Price = 45.00M, Id = 8, ItemsId = 4, OrderId = 11 },
                new Detail { Bill = 75.00M, Count = 3.000M, Price = 25.00M, Id = 9, ItemsId = 7, OrderId = 11 }

);
            });

        }

    }
}
