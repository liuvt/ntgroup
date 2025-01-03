using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ntgroup.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Biography = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDay = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DriveDetailTypes",
                columns: table => new
                {
                    drivetype_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivetype_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivetype_Price = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drivetype_Distance = table.Column<int>(type: "int", nullable: true),
                    drivetype_During = table.Column<int>(type: "int", nullable: true),
                    drivetype_DistanceMore = table.Column<int>(type: "int", nullable: true),
                    drivetype_DuringMore = table.Column<int>(type: "int", nullable: true),
                    drivetype_CollectArrears = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drivetype_Discount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drivetype_TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drivetype_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivetype_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    drivetype_DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveDetailTypes", x => x.drivetype_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    driver_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driver_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driver_EmployeeID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driver_Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driver_Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driver_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    driver_DeleteddAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.driver_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TimepiecesDTO",
                columns: table => new
                {
                    tp_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taxi_NumberId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taxi_NumberPlate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tp_TimeStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tp_TimeEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tp_Kilometer = table.Column<int>(type: "int", nullable: false),
                    tp_KilometerEmpty = table.Column<int>(type: "int", nullable: false),
                    tp_KilometerTotal = table.Column<int>(type: "int", nullable: false),
                    tp_Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    tp_StartPoint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tp_EndPoint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimepiecesDTO", x => x.tp_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WalletTransactionTypes",
                columns: table => new
                {
                    transactiontype_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transactiontype_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transactiontype_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactionTypes", x => x.transactiontype_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    car_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    car_NumberId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    car_NumberPlate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    car_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    car_DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    driver_Id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.car_Id);
                    table.ForeignKey(
                        name: "FK_Cars_Drivers_driver_Id",
                        column: x => x.driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "driver_Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    wallet_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wallet_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wallet_Balance = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    wallet_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    wallet_DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    driver_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.wallet_Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Drivers_driver_Id",
                        column: x => x.driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "driver_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shiftworks",
                columns: table => new
                {
                    sw_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sw_TimeStart = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    sw_TimeEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    sw_Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sw_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    sw_DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    car_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driver_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shiftworks", x => x.sw_Id);
                    table.ForeignKey(
                        name: "FK_Shiftworks_Cars_car_Id",
                        column: x => x.car_Id,
                        principalTable: "Cars",
                        principalColumn: "car_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shiftworks_Drivers_driver_Id",
                        column: x => x.driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "driver_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    transaction_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    transaction_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    transaction_DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    transactiontype_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wallet_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.transaction_Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_WalletTransactionTypes_transactiontype_Id",
                        column: x => x.transactiontype_Id,
                        principalTable: "WalletTransactionTypes",
                        principalColumn: "transactiontype_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_wallet_Id",
                        column: x => x.wallet_Id,
                        principalTable: "Wallets",
                        principalColumn: "wallet_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Timepieces",
                columns: table => new
                {
                    tp_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tp_TimeStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tp_TimeEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tp_Kilometer = table.Column<int>(type: "int", nullable: false),
                    tp_KilometerEmpty = table.Column<int>(type: "int", nullable: false),
                    tp_KilometerTotal = table.Column<int>(type: "int", nullable: false),
                    tp_Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    tp_StartPoint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tp_EndPoint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tp_CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tp_DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sw_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timepieces", x => x.tp_Id);
                    table.ForeignKey(
                        name: "FK_Timepieces_Shiftworks_sw_Id",
                        column: x => x.sw_Id,
                        principalTable: "Shiftworks",
                        principalColumn: "sw_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drives",
                columns: table => new
                {
                    drive_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drive_Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drive_Revenue = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drive_TotalRevenue = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    drive_CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    drive_DeletedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    driver_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_Id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drives", x => x.drive_Id);
                    table.ForeignKey(
                        name: "FK_Drives_Drivers_driver_Id",
                        column: x => x.driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "driver_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drives_WalletTransactions_driver_Id",
                        column: x => x.driver_Id,
                        principalTable: "WalletTransactions",
                        principalColumn: "transaction_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DriveDetails",
                columns: table => new
                {
                    drivedetail_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_PickUp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_DropOff = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_Distance = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_Price = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_CreateAt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_CompletedAt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivedetail_Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drivetype_Id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    driveType = table.Column<int>(type: "int", nullable: true),
                    drive_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveDetails", x => x.drivedetail_Id);
                    table.ForeignKey(
                        name: "FK_DriveDetails_Drives_drive_Id",
                        column: x => x.drive_Id,
                        principalTable: "Drives",
                        principalColumn: "drive_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "12/31/2024 3:25:07 PM", "Owner", "OWNER" },
                    { "2", "12/31/2024 3:25:07 PM", "Administrator", "ADMINISTRUCTOR" },
                    { "3", "12/31/2024 3:25:07 PM", "Manager", "MANAGER" },
                    { "4", "12/31/2024 3:25:07 PM", "Employee", "EMPLOYEE" },
                    { "5", "12/31/2024 3:25:07 PM", "Guest", "GUEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Biography", "BirthDay", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublishedAt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ADMIN-AUGCENTER-2023", 0, "", "", null, "2c9e4e29-b8de-47f4-808b-7f94fee0ba79", "administructor@augcenter.com", false, "Aug", "", "Center", false, null, "ADMINISTRUCTOR@AUGCENTER.COM", "ADMINISTRUCTOR@AUGCENTER.COM", "AQAAAAIAAYagAAAAEBrJfhm9d0MjkznlbL8r39fUqaYeKGUDkfNEJ00PQUNEAhRObKpq79uFOxFgWxe5FA==", "0868752251", false, null, "59730c11-c65a-4c12-a1c0-9a65585cf93e", false, "administructor@augcenter.com" },
                    { "EMPLOYEE-AUGCENTER-2023", 0, "", "", null, "516fe473-e92b-4113-9e19-e79b7e0a4e42", "employee@augcenter.com", false, "EMPLOYEE", "", "N/A", false, null, "EMPLOYEE@AUGCENTER.COM", "EMPLOYEE@AUGCENTER.COM", "AQAAAAIAAYagAAAAEFjY3VHKQtRXEh09KzVEh4W/WeAQ/qfvroaujr1oew6iRZH2/HschcFRgcYcRBDZ7Q==", "0868752251", false, null, "51ec8eca-9831-4069-8e39-3528351321b1", false, "employee@augcenter.com" },
                    { "GUEST-AUGCENTER-2023", 0, "", "", null, "047b3442-e9e5-45ce-8d91-e4a8f1d1a59b", "guester@augcenter.com", false, "GUEST", "", "N/A", false, null, "GUEST@AUGCENTER.COM", "GUEST@AUGCENTER.COM", "AQAAAAIAAYagAAAAEIn4iOFnno583REqYu64z5eBnJVy+KrEEmbSGgXMhsYQF34A9VkNQSCLCbCFs7oAPw==", "0868752251", false, null, "06bfdc18-c7ea-4db6-8377-c5011b4135ac", false, "guester@augcenter.com" },
                    { "OWNER-AUGCENTER-2023", 0, "", "", null, "15eeeca5-8d0f-40d1-814f-a3b15cfca67c", "owner@augcenter.com", false, "Aug", "", "Center", false, null, "OWNER@AUGCENTER.COM", "OWNER@AUGCENTER.COM", "AQAAAAIAAYagAAAAEH6kCPSA0riinIX5nPA/ULDIV+PWqbcFv2V9d9nZIVlusmzcAV/SA83eAp5zUWLQiA==", "0868752251", false, null, "de18d80a-0086-4652-9d15-fbedeccfc540", false, "owner@augcenter.com" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "driver_Id", "driver_Address", "driver_CreatedAt", "driver_DeleteddAt", "driver_EmployeeID", "driver_Name", "driver_Phone" },
                values: new object[,]
                {
                    { "DRIVER-0064", "", new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1673), new TimeSpan(0, 7, 0, 0, 0)), null, "0064", "CHUNG THANH CƯỜNG", "0328008459" },
                    { "DRIVER-0095", "", new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1776), new TimeSpan(0, 7, 0, 0, 0)), null, "0095", "ĐỒNG HOÀNG VỸ", "0888718961" },
                    { "DRIVER-0108", "", new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1779), new TimeSpan(0, 7, 0, 0, 0)), null, "0108", "ONG QUỐC THÁI", "0943141852" }
                });

            migrationBuilder.InsertData(
                table: "WalletTransactionTypes",
                columns: new[] { "transactiontype_Id", "transactiontype_Description", "transactiontype_Name" },
                values: new object[,]
                {
                    { "DEPOSIT", "Nạp tiền vào ví, để tăng số tiền hiện có", "Nạp tiền" },
                    { "PAYMENT", "Thanh toán tiền cuốc xe", "Thanh toán" },
                    { "WITHDRAW", "Rút tiền về tài khoản ngân hàng", "Rút tiền" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "ADMIN-AUGCENTER-2023" },
                    { "4", "EMPLOYEE-AUGCENTER-2023" },
                    { "5", "GUEST-AUGCENTER-2023" },
                    { "1", "OWNER-AUGCENTER-2023" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "car_Id", "car_CreatedAt", "car_DeletedAt", "car_NumberId", "car_NumberPlate", "driver_Id" },
                values: new object[,]
                {
                    { "BL3012", new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1842), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "BL3012", "94H-011.97", "DRIVER-0064" },
                    { "BL3014", new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1846), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "BL3014", "94H-011.36", "DRIVER-0095" },
                    { "BL3017", new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1852), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "BL3017", "94H-011.31", "DRIVER-0108" }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "wallet_Id", "driver_Id", "wallet_Balance", "wallet_CreatedAt", "wallet_DeletedAt", "wallet_Description" },
                values: new object[,]
                {
                    { "WALLET-001", "DRIVER-0108", 2000000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(1995), new TimeSpan(0, 7, 0, 0, 0)), null, "Trang thái tối thiếu" },
                    { "WALLET-002", "DRIVER-0095", 1500000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2006), new TimeSpan(0, 7, 0, 0, 0)), null, "Không đạt tối thiếu" },
                    { "WALLET-003", "DRIVER-0064", 50000000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2008), new TimeSpan(0, 7, 0, 0, 0)), null, "Tốt" }
                });

            migrationBuilder.InsertData(
                table: "WalletTransactions",
                columns: new[] { "transaction_Id", "transaction_Amount", "transaction_CreatedAt", "transaction_DeletedAt", "transaction_Description", "transactiontype_Id", "wallet_Id" },
                values: new object[,]
                {
                    { "3562881c-c214-4cbe-a874-dbea514a7c9a", 1000000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2195), new TimeSpan(0, 7, 0, 0, 0)), null, "Đã chạy xong", "DEPOSIT", "WALLET-002" },
                    { "70813cd5-5832-4f5f-b98a-d5217d8abfcc", 111000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2181), new TimeSpan(0, 7, 0, 0, 0)), null, "Đã chạy xong", "DEPOSIT", "WALLET-001" },
                    { "7fae474d-5a6e-4449-a149-6f354e6fd892", 1000000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2205), new TimeSpan(0, 7, 0, 0, 0)), null, "Đã chạy xong", "DEPOSIT", "WALLET-001" },
                    { "c302ffb3-5d26-4f1e-9173-0f5b40840dfe", 47000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2172), new TimeSpan(0, 7, 0, 0, 0)), null, "Đã chạy xong", "DEPOSIT", "WALLET-002" },
                    { "e06a3ef1-fbc6-4871-a896-3e78b127d8c3", 53000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2158), new TimeSpan(0, 7, 0, 0, 0)), null, "Đã chạy xong", "DEPOSIT", "WALLET-001" },
                    { "ef09703a-f068-4ba3-a649-3d2a38b12e5e", 35000m, new DateTimeOffset(new DateTime(2024, 12, 31, 15, 25, 8, 88, DateTimeKind.Unspecified).AddTicks(2177), new TimeSpan(0, 7, 0, 0, 0)), null, "Đã chạy xong", "DEPOSIT", "WALLET-003" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_driver_Id",
                table: "Cars",
                column: "driver_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriveDetails_drive_Id",
                table: "DriveDetails",
                column: "drive_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Drives_driver_Id",
                table: "Drives",
                column: "driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shiftworks_car_Id",
                table: "Shiftworks",
                column: "car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shiftworks_driver_Id",
                table: "Shiftworks",
                column: "driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Timepieces_sw_Id",
                table: "Timepieces",
                column: "sw_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_driver_Id",
                table: "Wallets",
                column: "driver_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_transactiontype_Id",
                table: "WalletTransactions",
                column: "transactiontype_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_wallet_Id",
                table: "WalletTransactions",
                column: "wallet_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DriveDetails");

            migrationBuilder.DropTable(
                name: "DriveDetailTypes");

            migrationBuilder.DropTable(
                name: "Timepieces");

            migrationBuilder.DropTable(
                name: "TimepiecesDTO");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Drives");

            migrationBuilder.DropTable(
                name: "Shiftworks");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "WalletTransactionTypes");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
