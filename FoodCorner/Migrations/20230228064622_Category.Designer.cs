﻿// <auto-generated />
using System;
using FoodCorner.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodCorner.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230228064622_Category")]
    partial class Category
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodCorner.Database.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categoryies", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Navbar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsViewFooter")
                        .HasColumnType("bit");

                    b.Property<bool>("IsViewHeader")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ToURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Navbars", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.ProductCatagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CatagoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CatagoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategoryies", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageNameFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPoster")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Backgroundİmage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundİmageInFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Button")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ButtonRedirectUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("HeaderTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sliders", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.SubNavbar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NavbarId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ToURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NavbarId");

                    b.ToTable("SubNavbars", (string)null);
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Category", b =>
                {
                    b.HasOne("FoodCorner.Database.Models.Category", "Parent")
                        .WithMany("Catagories")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("FoodCorner.Database.Models.ProductCatagory", b =>
                {
                    b.HasOne("FoodCorner.Database.Models.Category", "Catagory")
                        .WithMany("ProductCatagories")
                        .HasForeignKey("CatagoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodCorner.Database.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catagory");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FoodCorner.Database.Models.ProductImage", b =>
                {
                    b.HasOne("FoodCorner.Database.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FoodCorner.Database.Models.SubNavbar", b =>
                {
                    b.HasOne("FoodCorner.Database.Models.Navbar", "Navbar")
                        .WithMany("SubNavbars")
                        .HasForeignKey("NavbarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Navbar");
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Category", b =>
                {
                    b.Navigation("Catagories");

                    b.Navigation("ProductCatagories");
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Navbar", b =>
                {
                    b.Navigation("SubNavbars");
                });

            modelBuilder.Entity("FoodCorner.Database.Models.Product", b =>
                {
                    b.Navigation("ProductImages");
                });
#pragma warning restore 612, 618
        }
    }
}
