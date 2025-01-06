using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ntgroup.Data.Entities;


namespace ntgroup.Data;

public partial class ntgroupDbContext : IdentityDbContext<AppUser>
{
    //Get config in appsetting
    private readonly IConfiguration configuration;
    //Default constructor
    public ntgroupDbContext()
    {
    }

    //Constructor with parameter
    public ntgroupDbContext(DbContextOptions<ntgroupDbContext> options, IConfiguration _configuration) : base(options)
    {
        //Models - Etityties
        this.configuration = _configuration;
    }

    //Config to connection mysql server
    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(
                configuration["ConnectionStrings:Default"] ?? 
                                throw new InvalidOperationException("Can't found [Secret Key] in appsettings.json !"), 
                ServerVersion.Parse("8.0.31-mysql")
            );
        }
    }

    //Data seeding
    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Seeding Data bảng Roles
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole[] {
                    new IdentityRole
                    {
                        Id = "1",
                        Name = "Owner",
                        NormalizedName = """Owner""".ToUpper(),
                        ConcurrencyStamp = Convert.ToString(DateTime.Now)
                    },
                    new IdentityRole
                    {
                        Id = "2",
                        Name = "Administrator",
                        NormalizedName = """Administructor""".ToUpper(),
                        ConcurrencyStamp = Convert.ToString(DateTime.Now)
                    },
                    new IdentityRole
                    {
                        Id = "3",
                        Name = "Manager",
                        NormalizedName = """Manager""".ToUpper(),
                        ConcurrencyStamp = Convert.ToString(DateTime.Now)
                    },
                    new IdentityRole
                    {
                        Id = "4",
                        Name = "Employee",
                        NormalizedName = """Employee""".ToUpper(),
                        ConcurrencyStamp = Convert.ToString(DateTime.Now)
                    },
                    new IdentityRole
                    {
                        Id = "5",
                        Name = "Guest",
                        NormalizedName = """Guest""".ToUpper(),
                        ConcurrencyStamp = Convert.ToString(DateTime.Now)
                    },
            }
        );
        //Seeding Data bảng User
        var passwordHasher = new PasswordHasher<AppUser>();
        //Owner
        var userOwner = new AppUser
        {
            Id = "OWNER-AUGCENTER-2023",
            UserName = "owner@augcenter.com",
            Email = "owner@augcenter.com",
            NormalizedUserName = "OWNER@AUGCENTER.COM",
            FirstName = "Aug",
            LastName = "Center",
            NormalizedEmail = "OWNER@AUGCENTER.COM",
            PhoneNumber = "0868752251",
            LockoutEnabled = false
        };
        userOwner.PasswordHash = passwordHasher.HashPassword(userOwner, "Owner@123");
        //Admin
        var userAdmin = new AppUser
        {
            Id = "ADMIN-AUGCENTER-2023",
            UserName = "administructor@augcenter.com",
            Email = "administructor@augcenter.com",
            NormalizedUserName = "ADMINISTRUCTOR@AUGCENTER.COM",
            FirstName = "Aug",
            LastName = "Center",
            NormalizedEmail = "ADMINISTRUCTOR@AUGCENTER.COM",
            PhoneNumber = "0868752251",
            LockoutEnabled = false
        };
        userAdmin.PasswordHash = passwordHasher.HashPassword(userAdmin, "Admin@123");
        //Seller
        var userSeller = new AppUser
        {
            Id = "EMPLOYEE-AUGCENTER-2023",
            UserName = "employee@augcenter.com",
            Email = "employee@augcenter.com",
            NormalizedUserName = "EMPLOYEE@AUGCENTER.COM",
            FirstName = "EMPLOYEE",
            LastName = "N/A",
            NormalizedEmail = "EMPLOYEE@AUGCENTER.COM",
            PhoneNumber = "0868752251",
            LockoutEnabled = false
        };
        userSeller.PasswordHash = passwordHasher.HashPassword(userSeller, "Employeer@123");
        //Buyer
        var userBuyer = new AppUser
        {
            Id = "GUEST-AUGCENTER-2023",
            UserName = "guester@augcenter.com",
            Email = "guester@augcenter.com",
            NormalizedUserName = "GUEST@AUGCENTER.COM",
            FirstName = "GUEST",
            LastName = "N/A",
            NormalizedEmail = "GUEST@AUGCENTER.COM",
            PhoneNumber = "0868752251",
            LockoutEnabled = false
        };
        userBuyer.PasswordHash = passwordHasher.HashPassword(userBuyer, "Guester@123");
        modelBuilder.Entity<AppUser>().HasData(new AppUser[] { userOwner, userAdmin, userSeller, userBuyer });

        //Seeding Data bảng UserRole
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>[] {
                    new IdentityUserRole<string>
                    {
                        UserId = "OWNER-AUGCENTER-2023",
                        RoleId = "1",
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "ADMIN-AUGCENTER-2023",
                        RoleId = "2",
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "EMPLOYEE-AUGCENTER-2023",
                        RoleId = "4",
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "GUEST-AUGCENTER-2023",
                        RoleId = "5",
                    }
            }
        );
       
        //Do something
        base.OnModelCreating(modelBuilder);
    }
}

//Create mirations: dotnet ef migrations add Init -o Data/Migrations
//Create database: dotnet ef database update
//Publish project: dotnet publish -c Release --output ./Publish ntgroup.csproj