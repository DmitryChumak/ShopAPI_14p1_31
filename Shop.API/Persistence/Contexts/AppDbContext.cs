using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shop.API.Domain.Models;

namespace Shop.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Lastname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Password).IsRequired();
            builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);


            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.Name).IsRequired();
            builder.Entity<Role>().HasAlternateKey(x => x.Name);
            builder.Entity<Role>().HasMany(x => x.UserRoles).WithOne(x => x.Role);


            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });





            Role sa = new Role { Id = 1000, Name = "SuperAdmin" };
            Role admin = new Role { Id = 2000, Name = "Admin" };
            Role user = new Role { Id = 3000, Name = "User" };

            UserRole ur1 = new UserRole { UserId = 1000, RoleId = 1000 };
            UserRole ur2 = new UserRole { UserId = 1000, RoleId = 2000 };
            UserRole ur3 = new UserRole { UserId = 2000, RoleId = 3000 };
            UserRole ur4 = new UserRole { UserId = 2000, RoleId = 2000 };

            UserRole ur5 = new UserRole { UserId = 2001, RoleId = 3000 };
            UserRole ur6 = new UserRole { UserId = 2002, RoleId = 2000 };
            UserRole ur7 = new UserRole { UserId = 2003, RoleId = 1000 };
            UserRole ur8 = new UserRole { UserId = 2003, RoleId = 2000 };
            UserRole ur9 = new UserRole { UserId = 2004, RoleId = 2000 };



            User u1 = new User
            {
                Id = 1000,
                Name = "Natali",
                Lastname = "Enot",
                Login = "natathebest",
                Password = "iloveenot",
            };

            User u2 = new User
            {
                Id = 2000,
                Name = "Сергей",
                Lastname = "Новаятема",
                Login = "ineedtema",
                Password = "novayatema",
            };

            User u3 = new User
            {
                Id = 2001,
                Name = "John",
                Lastname = "Lee",
                Login = "lee",
                Password = "123",
            };

            User u4 = new User
            {
                Id = 2002,
                Name = "Sam",
                Lastname = "Smith",
                Login = "samsam",
                Password = "samsam",
            };

            User u5 = new User
            {
                Id = 2003,
                Name = "Billie",
                Lastname = "Cooper",
                Login = "admin",
                Password = "admin",
            };

            User u6 = new User
            {
                Id = 2004,
                Name = "Jack",
                Lastname = "Potter",
                Login = "potter",
                Password = "1",
            };

            builder.Entity<Role>().HasData(sa, admin, user);
            builder.Entity<UserRole>().HasData(ur1, ur2, ur3, ur4, ur5, ur6, ur7, ur8, ur9);
            builder.Entity<User>().HasData(u1, u2, u3,u4, u5, u6);

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Entity<Category>().HasData
            (
                new Category { Id = 1000, Name = "Notebook" },
                new Category { Id = 1001, Name = "Smartphone" },
                new Category { Id = 1002, Name = "Printer" },
                new Category { Id = 1003, Name = "TV" },
                new Category { Id = 1004, Name = "Headphones" },
                new Category { Id = 1005, Name = "Tablet" },
                new Category { Id = 1006, Name = "Camera" },
                new Category { Id = 1007, Name = "EBook Reader" }
            );

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.GoodCount).IsRequired();

            builder.Entity<Product>().HasData
            (
                new Product
                {
                    Id = 1001,
                    Name = "Dell ChromeBook Laptop NoteBook PC",
                    GoodCount = 1,
                    CategoryId = 1000
                },
                new Product
                {
                    Id = 1002,
                    Name = "Acer Aspire 5 Slim Laptop",
                    GoodCount = 13,
                    CategoryId = 1000
                },
                new Product
                {
                    Id = 1003,
                    Name = "HP 14in High Performance Laptop",
                    GoodCount = 12,
                    CategoryId = 1000
                },
                new Product
                {
                    Id = 1004,
                    Name = "ASUS VivoBook 15 Thin and Light Laptop",
                    GoodCount = 10,
                    CategoryId = 1000
                },
                new Product
                {
                    Id = 1005,
                    Name = "Lenovo 100E Chromebook 2ND Gen Laptop",
                    GoodCount = 10,
                    CategoryId = 1000
                },
                new Product
                {
                    Id = 1006,
                    Name = "Xiaomi Redmi Note 8",
                    GoodCount = 5,
                    CategoryId = 1001
                },
                new Product
                {
                    Id = 1007,
                    Name = "Samsung Galaxy A10",
                    GoodCount = 44,
                    CategoryId = 1001
                },
                new Product
                {
                    Id = 1008,
                    Name = "Brother Monochrome Black/White Laser Printer",
                    GoodCount = 2,
                    CategoryId = 1002
                },
                new Product
                {
                    Id = 1009,
                    Name = "HP LaserJet Pro M15w Wireless Laser Printer",
                    GoodCount = 8,
                    CategoryId = 1002
                },
                new Product
                {
                    Id = 1010,
                    Name = "TCL 32S325 32 Inch 720p Roku Smart LED TV",
                    GoodCount = 3,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1011,
                    Name = "VIZIO D-Series 24 Class Smart TV",
                    GoodCount = 10,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1012,
                    Name = "Westinghouse 50 4K Ultra HD Smart Roku TV",
                    GoodCount = 10,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1013,
                    Name = "LG OLED55B9PUA B9 Series 55 4K Ultra HD Smart OLED TV",
                    GoodCount = 11,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1014,
                    Name = "Samsung Electronics UN32J4001 32-Inch 720p LED TV",
                    GoodCount = 11,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1015,
                    Name = "VIZIO SmartCast D-Series 32-inch Class Smart Full-Array LED TV",
                    GoodCount = 12,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1016,
                    Name = "LG Electronics 22LJ4540 22-Inch 1080p IPS LED TV",
                    GoodCount = 50,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1017,
                    Name = "Hisense 32H5500F 32-Inch Android Smart TV",
                    GoodCount = 54,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1018,
                    Name = "LG Electronics 24LH4830-PU 24-Inch Smart LED TV",
                    GoodCount = 78,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1019,
                    Name = "Samsung Electronics UN32M4500BFXZA 720P Smart LED TV",
                    GoodCount = 90,
                    CategoryId = 1003
                },
                new Product
                {
                    Id = 1020,
                    Name = "Sony MDRZX110/BLK ZX Series Stereo Headphones",
                    GoodCount = 14,
                    CategoryId = 1004
                },
                new Product
                {
                    Id = 1021,
                    Name = "Bose QuietComfort 35 II Wireless Bluetooth Headphones",
                    GoodCount = 90,
                    CategoryId = 1004
                },
                new Product
                {
                    Id = 1022,
                    Name = "Samsung Galaxy Tab A 8.0 Wifi Android 9.0 Pie Tablet Black",
                    GoodCount = 20,
                    CategoryId = 1005
                },
                new Product
                {
                    Id = 1023,
                    Name = "Premium High Performance RCA Voyager Pro Touchscreen Tablet",
                    GoodCount = 20,
                    CategoryId = 1005
                },
                new Product
                {
                    Id = 1024,
                    Name = "Lenovo Tab M10 HD Android Tablet",
                    GoodCount = 95,
                    CategoryId = 1005
                },
                new Product
                {
                    Id = 1025,
                    Name = "Canon PowerShot SX420 Digital Camera",
                    GoodCount = 10,
                    CategoryId = 1006
                },
                 new Product
                 {
                     Id = 1026,
                     Name = "Kodak PIXPRO Friendly Zoom FZ53-BK 16MP Digital Camera",
                     GoodCount = 9,
                     CategoryId = 1006
                 },
                 new Product
                 {
                     Id = 1027,
                     Name = "Canon PowerShot ELPH 180 Digital Camera",
                     GoodCount = 40,
                     CategoryId = 1006
                 },
                 new Product
                 {
                     Id = 1028,
                     Name = "Canon PowerShot SX620 Digital Camera ",
                     GoodCount = 14,
                     CategoryId = 1006
                 },
                new Product
                {
                    Id = 1029,
                    Name = "Polaroid Originals OneStep 2 VF Instant Film Camera",
                    GoodCount = 15,
                    CategoryId = 1006
                },
                 new Product
                 {
                     Id = 1030,
                     Name = "Kobo Clara HD Carta E Ink Touchscreen E-Reader",
                     GoodCount = 10,
                     CategoryId = 1007
                 }

            );



        }


    }
}