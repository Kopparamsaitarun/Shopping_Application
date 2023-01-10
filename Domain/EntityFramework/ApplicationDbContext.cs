﻿using Domain.Model;
using Domain.Model.Cart;
using Domain.Model.Login;
using Domain.Model.OrderDetails;
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
        public virtual DbSet<UserOrderDetails> Order { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=MLBTAL-109698\\SQLEXPRESS;DataBase=ShoppingApp;Integrated Security=true;");
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
                entity.Property(t=>t.Role).IsRequired();    
            });

            oModelBuilder.Entity<Productlist>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Productlist");
                entity.Property(t => t.ProductDiscription).HasMaxLength(50);
                entity.Property(t => t.ProductPrice).IsRequired(); ;
                entity.Property(t => t.ProductName).IsRequired();
                entity.Property(t => t.ProductImage);
                entity.Property(e => e.InStock);
                entity.Property(e => e.InCart);
                entity.Property(e => e.Quantity);

            });

            oModelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.EmailId).IsRequired();
                entity.Property(t => t.Password).IsRequired();
                entity.Property(t => t.Role).IsRequired();

            });

            oModelBuilder.Entity<UserOrderDetails>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.EmailId).IsRequired();
                entity.Property(t => t.OrderId).IsRequired();
                entity.Property(t => t.ProductPrice).IsRequired();
                entity.Property(t => t.ProductId).IsRequired();
                entity.Property(t => t.ProductName).IsRequired();
                entity.Property(t => t.ProductImage).IsRequired();
                entity.Property(t => t.Quantity).IsRequired();
                entity.Property(t => t.Count).IsRequired();
            });
            OnModelCreatingPartial(oModelBuilder);
            //new EmployeeMap(oModelBuilder.Entity<Employee>());
            //new EmployeeProfessionalMap(oModelBuilder.Entity<EmployeeProfessional>());
            //new EmployeeQualificationMap(oModelBuilder.Entity<EmployeeQualification>());

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);      
        public DbSet<CartProducts> CartProducts { get; set; }
        public DbSet<Productlist> Productlist { get; set; }
    }
}

