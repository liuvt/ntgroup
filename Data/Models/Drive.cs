using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

//Tổng các cuốc xe của 1 tài xế theo mã tài xế va mã xe theo ngày
public class Drive 
{
    [Key]
    public string drive_Id {get; set;} = string.Empty; //Mã cuốc xe
    public decimal? drive_Amount {get; set;} //Giá theo ngày
    public decimal? drive_Revenue {get; set;} //Doanh thu ngày
    public decimal? drive_TotalRevenue {get; set;} //Doanh thu tổng
    public DateTimeOffset? drive_CreatedAt {get; set;} //Ngày tạo
    public DateTimeOffset? drive_DeletedAt {get; set;} //Ngày xóa
    public string driver_Id { get; set; } = string.Empty;
    [ForeignKey("driver_Id")]
    public Driver? driver { get; set; }

    //Danh sách chi tiết cuốc xe : 1 Nhiều với DriveDetails
    public List<DriveDetail>? drivedetails {get; set;} = new List<DriveDetail>(); 

    public string transaction_Id { get; set; } = string.Empty;
    [ForeignKey("driver_Id")]
    public WalletTransaction? transaction { get; set; }
}