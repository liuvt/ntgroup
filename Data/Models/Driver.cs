using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class Driver
{
    [Key]
    public string driver_Id { get; set; } = string.Empty;
    public string driver_Name { get; set; } = string.Empty;
    public string driver_EmployeeID { get; set; } = string.Empty;
    public string driver_Phone { get; set; } = string.Empty;
    public string driver_Address { get; set; } = string.Empty;
    public DateTimeOffset? driver_CreatedAt { get; set; }
    public DateTimeOffset? driver_DeleteddAt { get; set; }

    public Car? car { get; set; }
    public Wallet? wallet { get; set; }
}