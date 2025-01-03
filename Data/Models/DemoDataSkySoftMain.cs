
using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Models;
public class DemoDataSkySoftMain
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string NumberPlate { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty; 
    public string Static { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt {get; set; }
}