using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Entities;
public class TimepieceDTO
{
    [Key]
    public string tp_ID {get; set;} = string.Empty;
    public string taxi_NumberId { get; set; } = string.Empty; // Số hiệu taxi => có thể thay đổi xe
    public string taxi_NumberPlate { get; set; } = string.Empty; //Biển số
    public DateTime tp_TimeStart {get; set;}
    public DateTime tp_TimeEnd {get; set;}
    public int tp_Kilometer {get; set;}
    public int tp_KilometerEmpty {get; set;}
    public int tp_KilometerTotal {get; set;}
    public decimal tp_Amount {get; set;}
    public string tp_StartPoint {get; set; } = string.Empty; //Điểm đầu	
    public string tp_EndPoint {get; set; } = string.Empty; //Điểm cuối
}

