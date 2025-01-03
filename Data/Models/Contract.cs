using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class Contract
{
    [Key]
    public string contract_Id { get; set; } = string.Empty; //Mã hợp đồng
    public DateTime contract_Date { get; set; } //Ngày bắt đầu
    public string contract_Time { get; set; } = string.Empty; //Thời gian bắt đầu
    public string contract_IdTaxi { get; set; } = string.Empty; //Mã taxi
    public string contract_StartPoint { get; set; } = string.Empty; //Điểm xuất phát
    public string contract_EndPoint { get; set; } = string.Empty; //Điểm trả khách
    public string contract_Kilometer { get; set; } = string.Empty; //Số km
    public string contract_KeyTime {get; set;} = string.Empty; //Số giờ chờ quy định
    public string contract_Price {get; set;} = string.Empty; //Giá
    public string contract_DealPrice {get; set;} = string.Empty; //Giá thỏa thuận
    public string contract_Catalog {get; set;} = string.Empty; //SC - HĐ 1 chiều hoặc 2 chiều
    public string contract_Tip {get; set;} = string.Empty; //Khách ở: Bệnh Viện | Bến Tàu
    public string contract_TaxiGetTip {get; set;} = string.Empty; //Lái xe thu (Không hiểu mục đích)	
    public string contract_PeopleStart {get; set; } = string.Empty; //Người phát điểm: Lái xe | Khách
    public string contract_Type {get; set; } = string.Empty; //Loại hợp đồng: Hợp đồng thường | Tuyến chiến lược
    public string contract_Status {get; set; } = string.Empty; //Trạng thái	
    public string contract_Note {get; set; } = string.Empty; //Ghi chú
}

