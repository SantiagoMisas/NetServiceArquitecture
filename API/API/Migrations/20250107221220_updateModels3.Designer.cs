﻿// <auto-generated />
using System;
using API.ApplicationDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250107221220_updateModels3")]
    partial class updateModels3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AddressText")
                        .HasColumnType("longtext");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("API.Models.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AddressId")
                        .HasColumnType("longtext");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("API.Models.Address", b =>
                {
                    b.HasOne("API.Models.Person", "Person")
                        .WithOne("Address")
                        .HasForeignKey("API.Models.Address", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("API.Models.Person", b =>
                {
                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
