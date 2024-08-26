﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using demo_product.Data;

#nullable disable

namespace demo_product.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240812024715_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("demo_product.Entity.Account", b =>
                {
                    b.Property<int>("idAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idAccount"));

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("emailAccount")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("fullNameAccount")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("passwordAccount")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("userNameAccount")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("idAccount");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("demo_product.Entity.Order", b =>
                {
                    b.Property<int>("idOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idOrder"));

                    b.Property<string>("addressOrder")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("emailOrder")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("nameOrder")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("phoneOrder")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("idOrder");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("demo_product.Entity.OrderDetail", b =>
                {
                    b.Property<int>("idOrderDetail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idOrderDetail"));

                    b.Property<int>("idOrder")
                        .HasColumnType("int");

                    b.Property<int>("idProduct")
                        .HasColumnType("int");

                    b.Property<int>("statusOrderDetail")
                        .HasColumnType("int");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.Property<int>("totalQuantity")
                        .HasColumnType("int");

                    b.HasKey("idOrderDetail");

                    b.HasIndex("idOrder");

                    b.HasIndex("idProduct");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("demo_product.Entity.Product", b =>
                {
                    b.Property<int>("idProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idProduct"));

                    b.Property<string>("imgProduct")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameProduct")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("priceProduct")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<int>("quantityProduct")
                        .HasColumnType("int");

                    b.Property<int>("statusProduct")
                        .HasColumnType("int");

                    b.HasKey("idProduct");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("demo_product.Entity.OrderDetail", b =>
                {
                    b.HasOne("demo_product.Entity.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("idOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("demo_product.Entity.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("idProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("demo_product.Entity.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("demo_product.Entity.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}