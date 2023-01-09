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

            modelBuilder.Entity("Domain.Model.Product.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Model.Product.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ProductImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Model.Product.ProductCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("categoryId")
                        .HasColumnType("bigint");

                    b.Property<long?>("productId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("categoryId");

                    b.HasIndex("productId");

                    b.ToTable("ProductCategory");
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

                    b.HasOne("Domain.Model.Product.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId");

                    b.Navigation("product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Model.Product.ProductCategory", b =>
                {
                    b.HasOne("Domain.Model.Product.Category", "category")
                        .WithMany("Products")
                        .HasForeignKey("categoryId");

                    b.HasOne("Domain.Model.Product.Product", "product")
                        .WithMany("Categories")
                        .HasForeignKey("productId");

                    b.Navigation("category");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Domain.Model.Product.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Model.Product.Product", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
