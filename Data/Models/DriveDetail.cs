using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

// Thực hiện giảm giá tại đây // thay đổi các nội dung cuốc xe tại đây
public class DriveDetail
{
    // Cuốc xe thực tế từ SkySoft
    [Key]
    public string drivedetail_Id {get; set;} = string.Empty; 
    public string drivedetail_PickUp {get; set;} = string.Empty;
    public string drivedetail_DropOff {get; set;} = string.Empty;
    public string drivedetail_Distance {get; set;} = string.Empty; // Khoản cách
    public string drivedetail_Price {get; set;} = string.Empty; // Giá
    public string drivedetail_CreateAt {get; set;} = string.Empty; // Thời gian tạo 
    public string drivedetail_CompletedAt {get; set;} = string.Empty; // Thời gian kết thúc
    public string drivedetail_Description {get; set;} = string.Empty; 
    public string drivedetail_Status {get; set;} = string.Empty; 

    //Phân loại cuốc xe
    public string drivetype_Id {get; set;} = string.Empty; //Hợp đồng, cuốc lẻ, GSM, App Khach
    [ForeignKey("drivetype_Id")]
    public DriveType? driveType {get; set;} //Hợp đồng, cuốc lẻ, GSM, App Khach

    //Nhiều - một với Drive
    public string drive_Id {get; set;} = string.Empty; 
    [ForeignKey("drive_Id")]
    public Drive? drive {get; set;} 
}