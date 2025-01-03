
using System.ComponentModel.DataAnnotations;
using ntgroup.Data.Models;
namespace ntgroup.Data.Entities;

public class DriveDTO
{
    public string drive_Id {get; set;} = string.Empty; //Mã cuốc xe
    public decimal? drive_Amount {get; set;} //Giá theo ngày
    public decimal? drive_Revenue {get; set;} //Doanh thu ngày
    public decimal? drive_TotalRevenue {get; set;} //Doanh thu tổng
    public DateTimeOffset? drive_CreatedAt {get; set;} //Ngày tạo
    public DateTimeOffset? drive_DeletedAt {get; set;} //Ngày xóa
    public Driver? driver { get; set; }
    public List<DriveDetail>? drivedetails {get; set;} = new List<DriveDetail>(); 
    public WalletTransaction? transaction { get; set; }
}
