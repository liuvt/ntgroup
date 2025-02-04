
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class SpreadsRegisterContract
{
}

public class DefaultContract //Hợp đồng
{
    [Key]
    public string dc_Id { get; set; } = string.Empty;
    public string dc_DistanceOne { get; set; } = string.Empty; //so_km_1c
    public string dc_DistanceTwo { get; set; } = string.Empty; //so_km_2c
    public string dc_PriceOneFor5 { get; set; } = string.Empty; //gia_1c_5cho
    public string dc_PriceTwoFor5 { get; set; } = string.Empty; //gia_2c_5cho
    public string dc_PriceOneFor7 { get; set; } = string.Empty; //gia_1c_7cho
    public string dc_PriceTwoFor7 { get; set; } = string.Empty; //gia_2c_7cho
    public string dc_Time { get; set; } = string.Empty; // thoigian_cho_4cho
    public string dc_TimeFor7 { get; set; } = string.Empty;  // thoigian_cho_7cho
    public string dc_DecriptionFor4 { get; set; } = string.Empty; // show_4_cho
    public string dc_DecriptionFor7 { get; set; } = string.Empty; // show_7_cho
    public DateTime? CreatedAt {get; set; }
}

public class DefaultContractNew
{
    [Key]
    public string dc_Id { get; set; } = string.Empty;
    public string dc_Distance { get; set; } = string.Empty; //so_km_1c
    public string dc_Price {get; set;} = string.Empty;
    public string dc_Decription { get; set; } = string.Empty; // show_4_cho
    public DateTime? CreatedAt {get; set; }
    public DriveType? driveType {get; set;}
    public ContractType? contractType {get; set;}
}

public class DriveType //Loại xe
{
    [Key]
    public string dt_Id { get; set; } = string.Empty; // Mã
    public string dt_Name { get; set; } = string.Empty; // Tên xe : 4 chổ, 5chổ, 7 chổ
    public string dt_Decription { get; set; } = string.Empty; // Mô tả
    public DateTime? CreatedAt {get; set; }
    public string dc_Id {get; set;} = string.Empty;
}

public class ContractType //Loại xe
{
    [Key]
    public string ct_Id { get; set; } = string.Empty; // Mã
    public string ct_Name { get; set; } = string.Empty; // Loại: 1 chiều, 2 chiều
    public string ct_Decription { get; set; } = string.Empty; // Mô tả
    public DateTimeOffset? CreatedAt {get; set; }
    public string dc_Id {get; set;} = string.Empty;
}
