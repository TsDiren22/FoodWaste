﻿using FoodWaste.Domain;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FoodWaste.Infrastructure.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafeteria> Cafeterias { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            /*configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");*/
        }

        public byte[] ImageToByteArray(string src)
        {
            using (var ms = new MemoryStream())
            {
                if (File.Exists(src))
                {
                    Image img = Image.FromFile(src);
                    img.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
                else
                {
                    return new byte[0];
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().Property(x => x.CityOfStudy).HasConversion(x => x.ToString(), x => (City)Enum.Parse(typeof(City), x));
            

            modelBuilder.Entity<Cafeteria>().HasData(
                new Cafeteria { Id = 1, Location = "Lovensdijkstraat, LD000", Name = "Lovensdijkstraat Big Cafeteria", City = City.Breda, HasWarmMeals = true },
                new Cafeteria { Id = 2, Location = "Lovensdijkstraat, LA500", Name = "Lovensdijkstraat Small Cafeteria", City = City.Breda, HasWarmMeals = false },
                new Cafeteria { Id = 3, Location = "Hogeschoollaan, HD000", Name = "Hogeschoollaan Big Cafeteria", City = City.Breda, HasWarmMeals = true }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Diren Öztürk", CityOfStudy = City.Breda, DateOfBirth = new DateTime(), StudentNumber = 12345, Email = "ghi@ghi.com", PhoneNumber = "0612345678" },
                new Student { Id = 2, Name = "Jane Doe", CityOfStudy = City.Breda, DateOfBirth = new DateTime(), StudentNumber = 12345, Email = "diren_2002@hotmail.com", PhoneNumber = "0687654321" },
                new Student { Id = 3, Name = "John Smith", CityOfStudy = City.Breda, DateOfBirth = new DateTime(), StudentNumber = 12345, Email = "diren_2003@hotmail.com", PhoneNumber = "0654781234" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "John Doe", CafeteriaId = 1, EmployeeNumber = 12345, Email = "abc@abc.com" },
                new Employee { Id = 2, Name = "Jane Doe", CafeteriaId = 2, EmployeeNumber = 54321, Email = "bcd@bcd.com" },
                new Employee { Id = 3, Name = "John Smith", CafeteriaId = 3, EmployeeNumber = 45123, Email = "cde@cde.com" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, IsAlcoholic = false, Name = "Döner", Picture = ImageToByteArray(".\\wwwroot\\images\\doner.jpg") },
                new Product { Id = 2, IsAlcoholic = false, Name = "Frikandel", Picture = ImageToByteArray(".\\wwwroot\\images\\frikandel.jpg") },
                new Product { Id = 3, IsAlcoholic = true, Name = "Bier", Picture = ImageToByteArray(".\\wwwroot\\images\\bier.jpg")}
            );

            modelBuilder.Entity<Package>().HasData(
                new Package { Id = 1, CafeteriaId = 1, ContainsAdultProducts = false, MealType = "Warm", Name = "Warm Meal Deluxe", PickupDate = new DateTime(), PickupExpiry = new DateTime(), Price = 10.0M, StudentId = 1 },
                new Package { Id = 2, CafeteriaId = 2, ContainsAdultProducts = false, MealType = "Warm", Name = "Warm Breakfast Deluxe", PickupDate = new DateTime(), PickupExpiry = new DateTime(), Price = 10.0M, StudentId = 2 },
                new Package { Id = 3, CafeteriaId = 3, ContainsAdultProducts = true, MealType = "Cold", Name = "Cold Meal Deluxe", PickupDate = new DateTime(), PickupExpiry = new DateTime(), Price = 10.0M, StudentId = 3 },
                new Package { Id = 4, CafeteriaId = 1, ContainsAdultProducts = true, MealType = "Cold", Name = "Test1", PickupDate = new DateTime(), PickupExpiry = new DateTime(), Price = 10.0M},
                new Package { Id = 5, CafeteriaId = 1, ContainsAdultProducts = true, MealType = "Cold", Name = "Test2", PickupDate = new DateTime(), PickupExpiry = new DateTime(), Price = 10.0M},
                new Package { Id = 6, CafeteriaId = 1, ContainsAdultProducts = true, MealType = "Cold", Name = "Test3", PickupDate = new DateTime(), PickupExpiry = new DateTime(), Price = 10.0M}

            );
        }
    }
}