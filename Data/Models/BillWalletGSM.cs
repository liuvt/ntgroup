
using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Models;
public class BillWalletGSM
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty; //Mã xe
    public string Price { get; set; } = string.Empty; // Thành tiền
    public DateTimeOffset? CreatedAt {get; set; }
}