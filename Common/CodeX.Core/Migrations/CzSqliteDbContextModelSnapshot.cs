﻿// <auto-generated />
using System;
using CodeX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeX.Core.Migrations
{
    [DbContext(typeof(CzSqliteDbContext))]
    partial class CzSqliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CodeX.Core.Models.CzAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameFamily")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameGiven")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameUser")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneMain")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ServiceDateStart")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.ToTable("UserAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
