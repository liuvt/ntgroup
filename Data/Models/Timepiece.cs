using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class Timepiece
{
    [Key]
    public string tp_Id {get; set;} = string.Empty;
    public DateTime tp_TimeStart {get; set;}
    public DateTime tp_TimeEnd {get; set;}
    public int tp_Kilometer {get; set;}
    public int tp_KilometerEmpty {get; set;}
    public int tp_KilometerTotal {get; set;}
    public decimal tp_Amount {get; set;}
    public string tp_StartPoint {get; set; } = string.Empty; //Điểm đầu	
    public string tp_EndPoint {get; set; } = string.Empty; //Điểm cuối
    public DateTime tp_CreatedAt {get; set;}
    public DateTime tp_DeletedAt {get; set;}

    public string sw_Id {get; set;} = string.Empty; //Lấy thông tin từ tài xế, biết được xe đang chạy
    [ForeignKey("sw_Id")]
    public Shiftwork? shiftwork { get; set; }
}

