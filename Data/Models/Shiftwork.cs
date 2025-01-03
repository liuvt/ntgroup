using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class Shiftwork
{
    [Key]
    public string sw_Id { get; set; } = string.Empty; //Guid ID
    public DateTimeOffset? sw_TimeStart { get; set; } //Thời gian lên ca
    public DateTimeOffset? sw_TimeEnd { get; set; }  //Thời gian xuống ca   
    public string sw_Status { get; set; } = string.Empty;
    public DateTimeOffset? sw_CreatedAt { get; set; }
    public DateTimeOffset? sw_DeletedAt { get; set; }

    public string car_Id { get; set; } = string.Empty;
    [ForeignKey("car_Id")]
    public Car? car { get; set; }

    public string driver_Id { get; set; } = string.Empty;
    [ForeignKey("driver_Id")]
    public Driver? driver { get; set; }
}
