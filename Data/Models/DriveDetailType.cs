using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class DriveDetailType
{
    [Key]
    public string drivetype_Id {get; set;} = string.Empty; 
    public string drivetype_Name {get; set;} = string.Empty; // Hợp đồng, cuốc lẻ, GSM, App Khách
    //Hợp đồng
    public decimal? drivetype_Price {get; set; } //Giá quy định
    public int? drivetype_Distance {get; set; } //Khoản cách quy định
    public int? drivetype_During {get; set;} // Thời gian quy định
    public int? drivetype_DistanceMore {get; set; } // Vượt Khoản cách quy định
    public int? drivetype_DuringMore {get; set;} // Vượt Thời gian quy định
    public decimal? drivetype_CollectArrears {get; set;} //Truy thu
    public decimal? drivetype_Discount {get; set;} //Giảm giá
    public decimal? drivetype_TotalPrice {get; set;} // Thực thu = price + collectArrears - discount
    public string drivetype_Description {get; set;} = string.Empty;
    public DateTimeOffset? drivetype_CreatedAt {get; set;}
    public DateTimeOffset? drivetype_DeletedAt {get; set;}
}