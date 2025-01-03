using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Entities;
public class CustomerDTO
{
    [Key]
    public string customer_Type { get; set; } = string.Empty; //App khách hàng | GSM
    public string customer_Id { get; set; } = string.Empty; //ID	
    public string customer_Phone { get; set; } = string.Empty; //Điện thoại KH
    public string customer_Name { get; set; } = string.Empty; //Họ tên KH
    public string customer_Status { get; set; } = string.Empty; //Trạng thái
    public string customer_Kilometer { get; set; } = string.Empty; //Quãng đường (Km)
    public string customer_PhoneTaxi { get; set; } = string.Empty; //Điện thoại tài xế
    public string customer_IdTaxi { get; set; } = string.Empty; //Số tài
    public string customer_Price { get; set; } = string.Empty; //Tiền cước
    public string customer_Point { get; set; } = string.Empty; //Điểm đi và đến
    public string customer_DateTime { get; set; } = string.Empty; //Thời điểm đặt xe, lên/xuống xe
    public DateTime customner_CreateAt {get; set; }
}

