using System.ComponentModel.DataAnnotations.Schema;
using ntgroup.Extensions;

namespace ntgroup.Data.Models.Skysofts;

//Schema detail
public class Trip
{
    public string _id { get; set; } = string.Empty; // ID cuôc xe
    public string vehicleID { get; set; } = string.Empty; // ID xe
    public string state { get; set; } = string.Empty; 
    public string distance { get; set; } = string.Empty; // Khoản cách
    public string km { get; set; } = string.Empty; // KM có khách
    public string emptyKm { get; set; } = string.Empty; // KM rỗng
    public string charge { get; set; } = string.Empty; // Thù lao
    public string realCharge { get; set; } = string.Empty; // Nhập thù lao
    public string waitCharge { get; set; } = string.Empty; // Thù lao chờ
    public string waitTime { get; set; } = string.Empty; // Thời gian chờ
    public string unitPrice { get; set; } = string.Empty; 
    public string irState { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty; // Tên tài khoản thao tác
    public string typeID { get; set; } = string.Empty; // 0: đi theo đồng hồ, 1: đi theo hợp đồng, 2: đi theo App 
    public string tripID { get; set; } = string.Empty; // ID taxitrip
    public string token { get; set; } = string.Empty;
    public string pickupX { get; set; } = string.Empty;
    public string pickupY { get; set; } = string.Empty;
    public string dropOffX { get; set; } = string.Empty;
    public string dropOffY { get; set; } = string.Empty;
    public string? pickupDate { get; set; } 
    public string? dropOffDate { get; set; }
    public string? paidDate { get; set; } 
    public string plateNo { get; set; } = string.Empty; // Biễn số xe
    public string fromPlaceName { get; set; } = string.Empty; // Đia điểm khách lên xe đồng hồ tính tiền
    public string toPlaceName { get; set; } = string.Empty; // Địa điểm bấm dừng đồng hồ

    [ForeignKey("vehicleID")]
    public Vehicle? vehicle {get; set;} //  Số hiệu,Lấy biễn số,Lái xe
}

//Schema Total
public class TripTatol
{
    public List<Trip>? trips { get; set; }
    public int count => trips.Count();
    public string chargeTotal => SumString.SumListString<Trip>(trips, "charge");
    public string realChargeTotal => SumString.SumListString<Trip>(trips, "realCharge");
    public string actionResult { get; set; } = string.Empty;
    // Tính tổng các cột
}