using Domain.Model;
using Domain.Model.Cart;
using Domain.Model.Dashboard;
using Domain.Model.Order;
using Domain.Model.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityFramework
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        //public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }
        public virtual DbSet<User> Register { get; set; }
        public virtual DbSet<CartProducts> CartProducts { get; set; }
        public virtual DbSet<Productlist> Productlist { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderHeader> OrderHeader { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;DataBase=ShoppingApp;Integrated Security=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder oModelBuilder)
        {
            oModelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.firstName).IsRequired();
                entity.Property(t => t.lastName).IsRequired();
                entity.Property(t => t.email).IsRequired();
                entity.Property(t => t.phoneNumber).IsRequired();
                entity.Property(t => t.password).IsRequired();
                entity.Property(t => t.Role).IsRequired();
            });
            //Validations purpose 
            //oModelBuilder.Entity<Productlist>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.ToTable("Productlist");
            //    entity.Property(t => t.ProductDescription).HasMaxLength(50);
            //    entity.Property(t => t.ProductPrice).IsRequired(); ;
            //    entity.Property(t => t.ProductName).IsRequired();
            //    entity.Property(t => t.ProductName).IsRequired();
            //    entity.Property(t => t.ProductImage);
            //    entity.Property(e => e.InStock);
            //    entity.Property(e => e.InCart);
            //});

		


			OnModelCreatingPartial(oModelBuilder);
     
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);        
    }
}

