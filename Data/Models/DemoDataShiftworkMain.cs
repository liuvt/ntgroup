
using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Models;
public class DemoDataShiftworkMain
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty; 
    public string Date { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt {get; set; }
}