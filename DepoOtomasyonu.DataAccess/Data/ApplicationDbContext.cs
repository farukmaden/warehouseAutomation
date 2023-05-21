using DepoOtomasyonu.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepoOtomasyonu.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }    
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CardModel> Cards { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //users
            modelBuilder.Entity<UserModel>().HasKey(x => x.UserId);
            modelBuilder.Entity<UserModel>().Property(x => x.UserName).IsRequired();
            modelBuilder.Entity<UserModel>().Property(x => x.UserPassworrd).IsRequired();
            //Categories
            modelBuilder.Entity<CategoryModel>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<CategoryModel>().Property(x => x.CategoryName).IsRequired();
            //Customer
            modelBuilder.Entity<CustomerModel>().HasKey(x => x.CustomerId);
            modelBuilder.Entity<CustomerModel>().Property(x => x.CustomerName).IsRequired();
            modelBuilder.Entity<CustomerModel>().Property(x => x.CustomerPassword).IsRequired();
            //Card
            modelBuilder.Entity<CardModel>().HasKey(x=>x.CardId);
            modelBuilder.Entity<CardModel>().HasOne(x => x.CustomerModel).WithMany(x => x.CardModels).HasForeignKey(x=>x.CustomerId);
            modelBuilder.Entity<CardModel>().HasOne(x => x.ProductModel).WithMany(x => x.CardModels).HasForeignKey(x=>x.ProductId);
            //Products
            modelBuilder.Entity<ProductModel>().HasKey(x=>x.ProductId);
            modelBuilder.Entity<ProductModel>().Property(x => x.ProductCode).IsRequired();
            modelBuilder.Entity<ProductModel>().Property(x => x.ProductCount).IsRequired();
            modelBuilder.Entity<ProductModel>().Property(x => x.ProductPrice).IsRequired();
            modelBuilder.Entity<ProductModel>().Property(x => x.ProductPicture).IsRequired();
            modelBuilder.Entity<ProductModel>().HasOne(x=>x.CategoryModel).WithMany(x=>x.ProductModels).HasForeignKey(x=>x.CategoryId);
            //Orders
            modelBuilder.Entity<OrdersModel>().HasKey(x => x.OrdeId);
            modelBuilder.Entity<OrdersModel>().HasOne(x => x.CustomerModel).WithMany(x => x.OrdersModels).HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<OrdersModel>().HasOne(x => x.ProductModel).WithMany(x => x.OrdersModels).HasForeignKey(x => x.ProductId);
        }
    }
}
