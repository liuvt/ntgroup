
using System.ComponentModel.DataAnnotations;
using ntgroup.Data.Models;
namespace ntgroup.Data.Entities;

public class DriverDTO
{
    public string driver_Id { get; set; } = string.Empty;
    public string driver_Name { get; set; } = string.Empty;
    public string driver_EmployeeID { get; set; } = string.Empty;
    public string driver_Phone { get; set; } = string.Empty;
    public string driver_Address { get; set; } = string.Empty;
    public DateTimeOffset? driver_CreatedAt { get; set; }
    public DateTimeOffset? driver_DeleteddAt { get; set; }
    public CarDTO? car { get; set; } 
    public WalletDTO? wallet { get; set; }
    public List<DriveDTO>? drives { get; set; } = new List<DriveDTO>();
}

public class DriverCreateDTO
{
    [Required]
    public string driver_Name { get; set; } = string.Empty;
    [Required]
    public string driver_EmployeeID { get; set; } = string.Empty;
    [Required]
    public string driver_Phone { get; set; } = string.Empty;
    public string driver_Address { get; set; } = string.Empty;
}
