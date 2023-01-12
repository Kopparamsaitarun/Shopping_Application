﻿// <auto-generated />
using System;
using Domain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Model.Cart.CartProducts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("productId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("productId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("Domain.Model.Dashboard.Productlist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("InCart")
                        .HasColumnType("bit");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Productlist");
                });

            modelBuilder.Entity("Domain.Model.Order.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("orderNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("Domain.Model.User.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Domain.Model.User.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("policyFlag")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Register");
                });

            modelBuilder.Entity("Domain.Model.Cart.CartProducts", b =>
                {
                    b.HasOne("Domain.Model.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("Domain.Model.Dashboard.Productlist", "product")
                        .WithMany()
                        .HasForeignKey("productId");

                    b.Navigation("product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Model.Order.OrderDetail", b =>
                {
                    b.HasOne("Domain.Model.Dashboard.Productlist", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("Domain.Model.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Model.User.Address", b =>
                {
                    b.HasOne("Domain.Model.User.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
