﻿// <auto-generated />
using System;
using DLHAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DLHAPI.Migrations
{
    [DbContext(typeof(DLHDbContext))]
    [Migration("20230110173441_AddNewRecords")]
    partial class AddNewRecords
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DLHAPI.Models.DlhModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfExpire")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DlhModel");

                    b.HasData(
                        new
                        {
                            Id = 11,
                            Address = "York Street, New jersy",
                            DateOfExpire = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1284),
                            DateOfIssue = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1283),
                            Dob = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1251),
                            Name = "Test Name1"
                        },
                        new
                        {
                            Id = 1,
                            Address = "Washinton Dc",
                            DateOfExpire = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1289),
                            DateOfIssue = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1287),
                            Dob = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1286),
                            Name = "Test Name2"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Brooks Street",
                            DateOfExpire = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1293),
                            DateOfIssue = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1292),
                            Dob = new DateTime(2023, 1, 10, 10, 34, 41, 841, DateTimeKind.Local).AddTicks(1291),
                            Name = "Test Name3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
