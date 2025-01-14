
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocumentFormat.OpenXml.Presentation;

namespace ntgroup.Data.Models;

public class SpreadsChecker
{
}

public class ReportContract //Hợp đồng
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string DefaultDistance { get; set; } = string.Empty; // Khoản các mặc định
    public string OverDistance { get; set; } = string.Empty;  // Khoản cách vượt
    public string Surcharge { get; set; } = string.Empty; // Phụ phí
    public string Promotion { get; set; } = string.Empty; // Khuyến mãi
    public string TotalPrice { get; set; } = string.Empty; // Thành tiền
    public DateTimeOffset? CreatedAt { get; set; }
}

public class ReportTimepiece // Cuốc lẻ
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string Distance { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty; // Thành tiền
    public string PickUp { get; set; } = string.Empty;
    public string DropOut { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty; //Hợp đồng
    public DateTimeOffset? CreatedAt { get; set; }
}

public class ReportWalletGSM // Cuốc lẻ Ví GSM
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty; //Mã xe
    public string Price { get; set; } = string.Empty; // Thành tiền
    public DateTimeOffset? CreatedAt { get; set; }
}

public class ReportTotal // Báo cáo tổng và thông tin chuyển khoản
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string NumberDriver { get; set; } = string.Empty;
    public string RevenueTotal { get; set; } = string.Empty;
    public string RevenueByDate { get; set; } = string.Empty;
    public string QRContext { get; set; } = string.Empty;
    public string QRUrl { get; set; } = string.Empty;
    public string TotalPrice { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
}

/// <summary>
/// SKYSOFT
/// </summary>
public class SkySoftMain
{
    [Key]
    public string SkySoftId { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string NumberPlate { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Static { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
    public ShiftworkMain? shiftworkMain { get; set; }
}

/// <summary>
/// DANH SÁCH LÊN CA DOANH THU
/// </summary>
public class ShiftworkMain
{
    [Key]
    public string ShiftworkId { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
    protected string SkySoftId { get; set; } = string.Empty;
}