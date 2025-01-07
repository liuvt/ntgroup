﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ntgroup.Data;

#nullable disable

namespace ntgroup.Data.Migrations
{
    [DbContext(typeof(ntgroupDbContext))]
    partial class ntgroupDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "1/6/2025 3:12:58 PM",
                            Name = "Owner",
                            NormalizedName = "OWNER"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "1/6/2025 3:12:58 PM",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRUCTOR"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "1/6/2025 3:12:58 PM",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "4",
                            ConcurrencyStamp = "1/6/2025 3:12:58 PM",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "5",
                            ConcurrencyStamp = "1/6/2025 3:12:58 PM",
                            Name = "Guest",
                            NormalizedName = "GUEST"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "OWNER-AUGCENTER-2023",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "ADMIN-AUGCENTER-2023",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "EMPLOYEE-AUGCENTER-2023",
                            RoleId = "4"
                        },
                        new
                        {
                            UserId = "GUEST-AUGCENTER-2023",
                            RoleId = "5"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ntgroup.Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "OWNER-AUGCENTER-2023",
                            AccessFailedCount = 0,
                            Address = "",
                            Biography = "",
                            ConcurrencyStamp = "ae6d59b7-e393-4fb9-8309-28575d108f9f",
                            Email = "owner@augcenter.com",
                            EmailConfirmed = false,
                            FirstName = "Aug",
                            Gender = "",
                            LastName = "Center",
                            LockoutEnabled = false,
                            NormalizedEmail = "OWNER@AUGCENTER.COM",
                            NormalizedUserName = "OWNER@AUGCENTER.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEJ49xh87CufCQs5zb90lsf4uxQsjTT3rRCgbrFLMXfthCyfqmmMMbpek2hYzeUzSZg==",
                            PhoneNumber = "0868752251",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "427361c9-a06c-4087-b5a4-cd3d62e27993",
                            TwoFactorEnabled = false,
                            UserName = "owner@augcenter.com"
                        },
                        new
                        {
                            Id = "ADMIN-AUGCENTER-2023",
                            AccessFailedCount = 0,
                            Address = "",
                            Biography = "",
                            ConcurrencyStamp = "3900f1fd-41ac-4fd6-b8e9-f3d99d727af1",
                            Email = "administructor@augcenter.com",
                            EmailConfirmed = false,
                            FirstName = "Aug",
                            Gender = "",
                            LastName = "Center",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMINISTRUCTOR@AUGCENTER.COM",
                            NormalizedUserName = "ADMINISTRUCTOR@AUGCENTER.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEB71Vj+LAPDFIPa1C4eWalgjdfJlt2aMtUc27sAWPt+hceQGwsOt09+g5ivAcUTjug==",
                            PhoneNumber = "0868752251",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bcb7e88d-c630-40c8-a2ef-5976943c1751",
                            TwoFactorEnabled = false,
                            UserName = "administructor@augcenter.com"
                        },
                        new
                        {
                            Id = "EMPLOYEE-AUGCENTER-2023",
                            AccessFailedCount = 0,
                            Address = "",
                            Biography = "",
                            ConcurrencyStamp = "4c48b779-1d17-4324-bac6-1ee9398e6d44",
                            Email = "employee@augcenter.com",
                            EmailConfirmed = false,
                            FirstName = "EMPLOYEE",
                            Gender = "",
                            LastName = "N/A",
                            LockoutEnabled = false,
                            NormalizedEmail = "EMPLOYEE@AUGCENTER.COM",
                            NormalizedUserName = "EMPLOYEE@AUGCENTER.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHZIHiiz58amUETheDlxpVubmTcgM5QvSJfWg+4/cYH/vf/8VJ56Y2ekvPaNUBD0pQ==",
                            PhoneNumber = "0868752251",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8338995d-f561-4d8e-992a-56217842b91a",
                            TwoFactorEnabled = false,
                            UserName = "employee@augcenter.com"
                        },
                        new
                        {
                            Id = "GUEST-AUGCENTER-2023",
                            AccessFailedCount = 0,
                            Address = "",
                            Biography = "",
                            ConcurrencyStamp = "d52fe3e1-f834-4dde-83ea-fa5ab9302b3b",
                            Email = "guester@augcenter.com",
                            EmailConfirmed = false,
                            FirstName = "GUEST",
                            Gender = "",
                            LastName = "N/A",
                            LockoutEnabled = false,
                            NormalizedEmail = "GUEST@AUGCENTER.COM",
                            NormalizedUserName = "GUEST@AUGCENTER.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEBdYJlnX2S/8YgYQlSE4q2Exuyqe+S3AngOW8e3e3XnEy08Tj6bxhg4gVQ3IZkdMWA==",
                            PhoneNumber = "0868752251",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "42762364-8601-44e4-9cab-3c6b736a198d",
                            TwoFactorEnabled = false,
                            UserName = "guester@augcenter.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ntgroup.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ntgroup.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ntgroup.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ntgroup.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}