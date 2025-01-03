using System.ComponentModel.DataAnnotations;
using ntgroup.Data.Models;

namespace ntgroup.Data.Entities;
public class ShiftworkDTO
{
    public string sw_Id { get; set; } = string.Empty; //Guid ID
    public DateTimeOffset? sw_TimeStart { get; set; }
    public DateTimeOffset? sw_TimeEnd { get; set; }
    public string sw_Status { get; set; } = string.Empty;
    public DateTimeOffset? sw_CreatedAt { get; set; }
    public DateTimeOffset? sw_DeletedAt { get; set; }
    public string car_Id { get; set; } = string.Empty;
    public string driver_Id { get; set; }  = string.Empty;
    public List<Timepiece>? timepieces { get; set; }
}

public class ShiftworkCreateDTO
{
    public DateTimeOffset? sw_TimeStart { get; set; }
    public DateTimeOffset? sw_TimeEnd { get; set; }
    public string sw_Status { get; set; } = string.Empty;
    public DateTimeOffset? sw_CreatedAt { get; set; }
    public string car_Id { get; set; } = string.Empty;
    public string driver_Id { get; set; }  = string.Empty;
}