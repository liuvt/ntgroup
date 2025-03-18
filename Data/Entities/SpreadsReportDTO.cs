
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using ntgroup.Data.Models;

namespace ntgroup.Data.Entities;
public class SpreadsReportDTO
{
}

public class StatisticalReportDTO
{
    public string id {get; set;} = string.Empty;
    public string cashbasis {get; set;} = string.Empty;
    public string revenue {get; set;} = string.Empty;
    public int records {get; set;}
    public string createdAt {get; set;} = string.Empty;
    public List<StatisticalReportDetailDTO>? statisticalReportDetails {get; set;} 
    public Deduct? deduct {get; set;}
}


public class StatisticalReportDetailDTO {
    public string thang_nam {get; set;} = string.Empty;
    public string bks_sotai {get; set;} = string.Empty;
    public string msnv {get; set;} = string.Empty;
    public string hoten_laixe {get; set;} = string.Empty;
    public decimal? doanh_thu {get; set;}
    public decimal? tien_phai_thu {get; set;}
    public string so_tai {get; set;} = string.Empty;
    public string loaihinh_hoptac {get; set;} = string.Empty;
}
