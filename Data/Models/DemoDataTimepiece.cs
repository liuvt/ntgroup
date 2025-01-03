
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class DemoDataTimepiece
{
    [Key]
    public string Id { get; set; } = string.Empty; 
    public string NumberCar { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty; 
    public string Distance { get; set; } = string.Empty; 
    public string Amount { get; set; } = string.Empty; // Thành tiền
    public string PickUp { get; set; } = string.Empty;
    public string DropOut { get; set; } = string.Empty; 
    public string Note { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty; //Hợp đồng
    public DateTimeOffset? CreatedAt {get; set; }
}