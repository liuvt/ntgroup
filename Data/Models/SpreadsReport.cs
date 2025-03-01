
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class SpreadsReport
{
}

public class StatisticalReportTotal
{
    public string id {get; set;} = string.Empty;
    public string cashbasis {get; set;} = string.Empty;
    public string revenue {get; set;} = string.Empty;
    public int records {get; set;}
    public string createdAt {get; set;} = string.Empty;
    public List<StatisticalReport>? statisticalReports {get; set;} 
}

public class StatisticalReport {
    public string id {get; set;} = string.Empty;
    public string stt {get; set;} = string.Empty;
    public string thoi_gian_tao {get; set;} = string.Empty;
    public string thang_nam {get; set;} = string.Empty;
    public string bks_sotai {get; set;} = string.Empty;
    public string msnv {get; set;} = string.Empty;
    public string hoten_laixe {get; set;} = string.Empty;
    public string hoten_msnv {get; set;} = string.Empty;
    public string doanh_thu {get; set;} = string.Empty;
    public string so_cuoc {get; set;} = string.Empty;
    public string sokm_vandoanh {get; set;} = string.Empty;
    public string sokm_cokhach {get; set;} = string.Empty;
    public string tru_tien {get; set;} = string.Empty;
    public string phu_thu {get; set;} = string.Empty;
    public string songay_nhap_doanhthu {get; set;} = string.Empty;
    public string ghi_chu {get; set;} = string.Empty;
    public string km_taplo {get; set;} = string.Empty;
    public string tien_phai_thu {get; set;} = string.Empty;
    public string so_tai {get; set;} = string.Empty;
    public string phi_qua_tram {get; set;} = string.Empty;
    public string socuoc_hopdong {get; set;} = string.Empty;
    public string socuoc_gsm {get; set;} = string.Empty;
    public string loaihinh_hoptac {get; set;} = string.Empty;
    public string tru_tour {get; set;} = string.Empty;
    public string tru_cupco {get; set;} = string.Empty;
    public string tru_appkh {get; set;} = string.Empty;
    public string truythu_km {get; set;} = string.Empty;
    public string truythu_gsm {get; set;} = string.Empty;
    public string truythu_appkh {get; set;} = string.Empty;
    public string ve_vi_gsm {get; set;} = string.Empty;
    public string gsm_10_phantram {get; set;} = string.Empty;
    public string tru_cuocxe_qua5h {get; set;} = string.Empty;
    public string tru_km_gsm {get; set;} = string.Empty;
    public string tru_km_appkh {get; set;} = string.Empty;
    public string tru_khac {get; set;} = string.Empty;
    public string truy_thu_thuc_thu {get; set;} = string.Empty;
    public string truy_thu_gio_cho {get; set;} = string.Empty;
    public string truy_thu_khac {get; set;} = string.Empty;
    public string baixe {get; set;} = string.Empty;
    public string tru_chenhlech_gsm {get; set;} = string.Empty;
    public string hinhthuc_kinhdoanh {get; set;} = string.Empty;
    public string tratruoc_trasau {get; set;} = string.Empty;
}
