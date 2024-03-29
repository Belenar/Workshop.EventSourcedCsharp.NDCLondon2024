﻿// <auto-generated />
using System;
using BeerSender.Web.Read_database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    [DbContext(typeof(Read_context))]
    partial class Read_contextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeerSender.Web.Read_database.Box_status", b =>
                {
                    b.Property<Guid>("Aggregate_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number_of_bottles")
                        .HasColumnType("int");

                    b.Property<int>("Shipment_status")
                        .HasColumnType("int");

                    b.HasKey("Aggregate_id");

                    b.ToTable("Box_statuses");
                });

            modelBuilder.Entity("BeerSender.Web.Read_database.Projection_checkpoint", b =>
                {
                    b.Property<string>("Projection_name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Event_version")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Projection_name");

                    b.ToTable("Projection_checkpoints");
                });
#pragma warning restore 612, 618
        }
    }
}
