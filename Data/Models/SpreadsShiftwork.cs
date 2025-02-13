
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class SpreadsShiftwork
{
}

public class Shiftwork
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string NumberCar { get; set; } = string.Empty;
    public string NumberDriver { get; set; } = string.Empty;
    public string ShiftworkDate { get; set; } = string.Empty;
    public string ShiftworkTime { get; set; } = string.Empty;
    public string ShiftworkType { get; set; } = string.Empty;
    public string ShiftworkStatus { get; set; } = string.Empty;
    public string ShiftworkNote { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
}

public class Drive
{
    [Key]
    public string drive_Id { get; set; } = string.Empty;
    public string drive_Plate { get; set; } = string.Empty;
    public string drive_Name { get; set; } = string.Empty;
    public string drive_Type { get; set; } = string.Empty;
    public string drive_Static { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
}