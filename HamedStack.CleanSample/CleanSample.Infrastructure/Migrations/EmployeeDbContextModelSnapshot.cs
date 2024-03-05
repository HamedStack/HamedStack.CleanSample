﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CleanSample.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanSample.Infrastructure.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    partial class EmployeeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("CleanSample.Domain.AggregateRoots.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReportsTo")
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "CleanSample.Domain.AggregateRoots.Employee.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "CleanSample.Domain.AggregateRoots.Employee.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("LastName");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Gender", "CleanSample.Domain.AggregateRoots.Employee.Gender#Gender", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<long>("Value")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Gender");
                        });

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("CleanSample.SharedKernel.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ProcessedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("CleanSample.Domain.AggregateRoots.Employee", b =>
                {
                    b.HasOne("CleanSample.Domain.AggregateRoots.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.OwnsOne("CleanSample.Domain.ValueObjects.Phone", "Fax", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Fax");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("CleanSample.Domain.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Phone");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("CleanSample.Domain.Enumerations.EmployeeStatus", "EmployeeStatus", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<long>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("INTEGER")
                                .HasColumnName("EmployeeStatus");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("CleanSample.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Street");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("CleanSample.Domain.ValueObjects.Title", "Title", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Title");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.Navigation("Address");

                    b.Navigation("EmployeeStatus");

                    b.Navigation("Fax");

                    b.Navigation("Manager");

                    b.Navigation("Phone");

                    b.Navigation("Title");
                });
#pragma warning restore 612, 618
        }
    }
}
