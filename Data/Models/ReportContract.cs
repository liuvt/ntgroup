using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class ReportContract
{
    [Key]
    public string reportcontract_IdTaxi { get; set; } = string.Empty; //Mã taxi | Số hiệu | Số tài
    public string reportcontract_NumberTaxi { get; set; } = string.Empty; //Biển số
    public string reportcontract_NameTaxi { get; set; } = string.Empty; //Lái xe | Tên tài xế
    public string reportcontract_TimeStart { get; set; } = string.Empty; //Thời gian bắt đầu
    public string reportcontract_TimeEnd { get; set; } = string.Empty; //Thời gian kết thúc
    public string reportcontract_Km { get; set; } = string.Empty; //Số km có khách
    public string reportcontract_KmEmpty { get; set; } = string.Empty; //Số km không có khách
    public string reportcontract_KmTotal { get; set; } = string.Empty; //Tổng số km
    public string reportcontract_TimeWait { get; set; } = string.Empty; //Thời gian chờ (p)
    public string reportcontract_CostWait { get; set; } = string.Empty; //Phí chờ
    public string reportcontract_CostTotal { get; set; } = string.Empty; //Thành tiền
    public string reportcontract_CostTaking { get; set; } = string.Empty;  //Thực thu
    public string reportcontract_CostTakingType { get; set; } = string.Empty; //Hình thức thanh toán : Đi theo đồng hồ | Đi theo hợp đồng
    public string reportcontract_PointStart {get; set; } = string.Empty; //Điểm đầu	
    public string reportcontract_PointEnd {get; set; } = string.Empty; //Điểm cuối
}

