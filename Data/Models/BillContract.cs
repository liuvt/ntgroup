
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class BillContract
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
    public DateTimeOffset? CreatedAt {get; set; }
}
