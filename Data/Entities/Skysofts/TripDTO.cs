using System.ComponentModel.DataAnnotations.Schema;
using DocumentFormat.OpenXml.Office2010.Excel;
using ntgroup.Data.Models;
using ntgroup.Data.Models.Skysofts;

namespace ntgroup.Data.Entities.Skysofts;

public class TripDTO
{
    public string _id { get; set; } = string.Empty;
    public string vehicleID { get; set; } = string.Empty; //Lấy thông tin xe qua ID
    public string userName { get; set; } = string.Empty; //Tên tài khoản thao tác
    public string vehicleCode { get; set; } = string.Empty;
    public string plateNo { get; set; } = string.Empty;
    public string staffName { get; set; } = string.Empty;
    public string? pickupDate { get; set; } 
    public string? dropOffDate { get; set; }
    public string km { get; set; } = string.Empty;
    public string emptyKm { get; set; } = string.Empty;
    public string totalKm { get; set; } = string.Empty;
    public string waitTime { get; set; } = string.Empty; // Thời gian chờ
    public string waitCharge { get; set; } = string.Empty; // Thù lau chờ
    public string charge { get; set; } = string.Empty; // Thù lao
    public string realCharge { get; set; } = string.Empty; // Nhập thực thù lao
    public string type { get; set; } = string.Empty; // 0: đi theo đồng hồ, 1: đi theo hợp đồng, 2: đi theo App
    public string fromPlaceName { get; set; } = string.Empty;
    public string toPlaceName { get; set; } = string.Empty;

    [ForeignKey("vehicleID")]
    public Vehicle? vehicle {get; set;} //  Số hiệu,Lấy biễn số,Lái xe
}
