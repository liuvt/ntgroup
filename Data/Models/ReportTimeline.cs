using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Models;
public class ReportTimeline
{
    [Key]
    public string rptimeline_IdTaxi { get; set; } = string.Empty; //Mã taxi | Số hiệu | Số tài
    public string rptimeline_NumberTaxi { get; set; } = string.Empty; //Biển số
    public string rptimeline_TimeStart { get; set; } = string.Empty; //Thời gian bắt đầu
    public string rptimeline_TimeEnd { get; set; } = string.Empty; //Thời gian kết thúc
    public string rptimeline_Km { get; set; } = string.Empty; //Số km có khách
    public string rptimeline_KmEmpty { get; set; } = string.Empty; //Số km không có khách
    public string rptimeline_KmTotal { get; set; } = string.Empty; //Tổng số km
    public string rptimeline_CostTotal { get; set; } = string.Empty; //Thành tiền
    public string rptimeline_PointStart {get; set; } = string.Empty; //Điểm đầu	
    public string rptimeline_PointEnd {get; set; } = string.Empty; //Điểm cuối
    public DateTime rptimeline_CreateAt {get; set;}
}

