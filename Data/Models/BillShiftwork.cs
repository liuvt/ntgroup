
using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Models;
public class BillShiftwork
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
    public DateTimeOffset? CreatedAt {get; set; }
}