
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class DemoDataShiftwork
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty; //Mã xe
    public string NumberDriver { get; set; } = string.Empty; //Mã nv
    public string RevenueTotal { get; set; } = string.Empty; //Doanh thu
    public string RevenueByDate { get; set; } = string.Empty;  //Doanh thu theo ngày
    public string QRContext { get; set; } = string.Empty; 
    public string QRUrl { get; set; } = string.Empty; 
    public string TotalPrice { get; set; } = string.Empty; // Thực thu
    public DateTimeOffset? CreatedAt {get; set; }
}