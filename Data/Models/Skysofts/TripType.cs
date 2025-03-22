using System.ComponentModel.DataAnnotations.Schema;
using ntgroup.Extensions;

namespace ntgroup.Data.Models.Skysofts;

// Cuốc xe app, cuốc xe đồng hồ, cuốc xe hợp đồng
// 0: Đi theo đồng hồ
// 1: Đi theo hợp đồng
// 4: App to App
public class TripType
{
    public int id { get; set; } 
    public string typeName { get; set; }
    public string typeStatic { get; set; }
    public string createdAt { get; set; }
}
