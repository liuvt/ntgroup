using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class Car
{
    [Key]
    public string car_Id { get; set; } = string.Empty; //Guid ID
    public string car_NumberId { get; set; } = string.Empty; // Số hiệu taxi => có thể thay đổi xe
    public string car_NumberPlate { get; set; } = string.Empty; //Biển số
    public DateTimeOffset car_CreatedAt {get ; set;}
    public DateTimeOffset car_DeletedAt {get ; set;}
    public string? driver_Id { get; set; } = string.Empty;
    [ForeignKey("driver_Id")]
    public Driver? driver { get; set; } 
}
