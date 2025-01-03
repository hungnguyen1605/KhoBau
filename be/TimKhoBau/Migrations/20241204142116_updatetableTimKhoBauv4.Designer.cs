﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimKhoBau.Data;

#nullable disable

namespace TimKhoBau.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241204142116_updatetableTimKhoBauv4")]
    partial class updatetableTimKhoBauv4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TimKhoBau.Model.KhoBau.KhobauEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("Columns")
                        .HasColumnType("int")
                        .HasColumnName("Columns");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Matrix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("matrix");

                    b.Property<int>("P")
                        .HasColumnType("int")
                        .HasColumnName("p");

                    b.Property<int>("Rows")
                        .HasColumnType("int")
                        .HasColumnName("rows");

                    b.Property<double>("TotalFuel")
                        .HasColumnType("float")
                        .HasColumnName("total_fuel");

                    b.HasKey("Id");

                    b.ToTable("kho_bau_entity");
                });
#pragma warning restore 612, 618
        }
    }
}
