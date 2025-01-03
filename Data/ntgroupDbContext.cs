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

    //Demo wallet
    public virtual DbSet<Car> Cars { get; set; } = null!; 
    public virtual DbSet<Driver> Drivers { get; set; } = null!;
    public virtual DbSet<Drive> Drives { get; set; } = null!;
    public virtual DbSet<DriveDetail> DriveDetails { get; set; } = null!;
    public virtual DbSet<DriveDetailType> DriveDetailTypes { get; set; } = null!;
    public virtual DbSet<Wallet> Wallets { get; set; } = null!;
    public virtual DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;
    public virtual DbSet<WalletTransactionType> WalletTransactionTypes { get; set; } = null!;

    public virtual DbSet<TimepieceDTO> TimepiecesDTO { get; set; } = null!;
    public virtual DbSet<Shiftwork> Shiftworks { get; set; } = null!;
    public virtual DbSet<Timepiece> Timepieces { get; set; } = null!;

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

        //Seeding Data bảng Driver
        modelBuilder.Entity<Driver>().HasData(
            new Driver[] {
                    new Driver
                    {
                        driver_Id = "DRIVER-0064",
                        driver_Name = "CHUNG THANH CƯỜNG",
                        driver_EmployeeID = "0064", 
                        driver_Phone = "0328008459",
                        driver_CreatedAt = DateTime.Now,
                    },
                    new Driver
                    {
                        driver_Id = "DRIVER-0095",
                        driver_Name = "ĐỒNG HOÀNG VỸ",
                        driver_EmployeeID = "0095",
                        driver_Phone = "0888718961",
                        driver_CreatedAt = DateTime.Now,
                    },
                    new Driver
                    {
                        driver_Id = "DRIVER-0108",
                        driver_Name = "ONG QUỐC THÁI",
                        driver_EmployeeID = "0108",
                        driver_Phone = "0943141852",
                        driver_CreatedAt = DateTime.Now,
                    },
            }
        );

        //Seeding Data bảng Car
        modelBuilder.Entity<Car>()
        .HasData(
            new Car[] {
                    new Car
                    {
                        car_Id = "BL3012",
                        car_NumberId = "BL3012",
                        car_NumberPlate= "94H-011.97",
                        car_CreatedAt = DateTime.Now,
                        driver_Id = "DRIVER-0064",
                    },
                    new Car
                    {
                        car_Id = "BL3014",
                        car_NumberId = "BL3014",
                        car_NumberPlate= "94H-011.36",
                        car_CreatedAt = DateTime.Now,
                        driver_Id = "DRIVER-0095",
                    },
                    new Car
                    {
                        car_Id = "BL3017",
                        car_NumberId = "BL3017",
                        car_NumberPlate= "94H-011.31",
                        car_CreatedAt = DateTime.Now,
                        driver_Id = "DRIVER-0108",
                    }
            }
        );

        //Seeding Wallet
        modelBuilder.Entity<Wallet>().HasData(
            new Wallet[] {
                    new Wallet
                    {
                        wallet_Id = "WALLET-001",
                        wallet_Balance = 2000000,
                        wallet_CreatedAt = DateTime.Now,
                        wallet_Description = "Trang thái tối thiếu",
                        driver_Id = "DRIVER-0108"
                    },
                    new Wallet
                    {
                        wallet_Id = "WALLET-002",
                        wallet_Balance = 1500000,
                        wallet_CreatedAt = DateTime.Now,
                        wallet_Description = "Không đạt tối thiếu",
                        driver_Id = "DRIVER-0095"
                    },
                    new Wallet
                    {
                        wallet_Id = "WALLET-003",
                        wallet_Balance = 50000000,
                        wallet_CreatedAt = DateTime.Now,
                        wallet_Description = "Tốt",
                        driver_Id = "DRIVER-0064"
                    }
            }
        );

        
        
        //Seeding Wallet
        modelBuilder.Entity<WalletTransactionType>().HasData(
            new WalletTransactionType[] {
                    new WalletTransactionType
                    {
                        transactiontype_Id = "DEPOSIT",
                        transactiontype_Name = "Nạp tiền",
                        transactiontype_Description = "Nạp tiền vào ví, để tăng số tiền hiện có"
                    },
                    new WalletTransactionType
                    {
                        transactiontype_Id = "WITHDRAW",
                        transactiontype_Name = "Rút tiền",
                        transactiontype_Description = "Rút tiền về tài khoản ngân hàng"
                    },
                    new WalletTransactionType
                    {
                        transactiontype_Id = "PAYMENT",
                        transactiontype_Name = "Thanh toán",
                        transactiontype_Description = "Thanh toán tiền cuốc xe"
                    }
            }
        );

        //Seeding Wallet
        modelBuilder.Entity<WalletTransaction>().HasData(
            new WalletTransaction[] {
                    new WalletTransaction
                    {
                        transaction_Id = Guid.NewGuid().ToString(),
                        transactiontype_Id = "DEPOSIT",
                        transaction_Amount = 53000,
                        transaction_Description = "Đã chạy xong",
                        transaction_CreatedAt = DateTime.Now,
                        wallet_Id =  "WALLET-001"
                    },
                    new WalletTransaction
                    {
                        transaction_Id = Guid.NewGuid().ToString(),
                        transactiontype_Id = "DEPOSIT",
                        transaction_Amount = 47000,
                        transaction_Description = "Đã chạy xong",
                        transaction_CreatedAt = DateTime.Now,
                        wallet_Id =  "WALLET-002"
                    },
                    new WalletTransaction
                    {
                        transaction_Id = Guid.NewGuid().ToString(),
                        transactiontype_Id = "DEPOSIT",
                        transaction_Amount = 35000,
                        transaction_Description = "Đã chạy xong",
                        transaction_CreatedAt = DateTime.Now,
                        wallet_Id =  "WALLET-003"
                    },
                    new WalletTransaction
                    {
                        transaction_Id = Guid.NewGuid().ToString(),
                        transactiontype_Id = "DEPOSIT",
                        transaction_Amount = 111000,
                        transaction_Description = "Đã chạy xong",
                        transaction_CreatedAt = DateTime.Now,
                        wallet_Id =  "WALLET-001"
                    },
                    new WalletTransaction
                    {
                        transaction_Id = Guid.NewGuid().ToString(),
                        transactiontype_Id = "DEPOSIT",
                        transaction_Amount = 1000000,
                        transaction_Description = "Đã chạy xong",
                        transaction_CreatedAt = DateTime.Now,
                        wallet_Id =  "WALLET-002"
                    },
                    new WalletTransaction
                    {
                        transaction_Id = Guid.NewGuid().ToString(),
                        transactiontype_Id = "DEPOSIT",
                        transaction_Amount = 1000000,
                        transaction_Description = "Đã chạy xong",
                        transaction_CreatedAt = DateTime.Now,
                        wallet_Id =  "WALLET-001"
                    }
            }
        );

        //Do something
        base.OnModelCreating(modelBuilder);
    }
}

//Create mirations: dotnet ef migrations add Init -o Data/Migrations
//Create database: dotnet ef database update
//Publish project: dotnet publish -c Release --output ./Publish IdentityBlazorCoreAPI.csproj