
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ntgroup.Extensions;

namespace ntgroup.Data.Models;
public class SpreadsReport
{
}

// Doanh thu tổng, đếm số bản ghi
public class StatisticalReport
{
    public string id {get; set;} = string.Empty;
    public string cashbasis {get; set;} = string.Empty;
    public string revenue {get; set;} = string.Empty;
    public int records {get; set;}
    public string createdAt {get; set;} = string.Empty;
    public List<StatisticalReportDetail>? statisticalReportDetails {get; set;} 
}

// Chi tiết doanh thu của lái xe
public class StatisticalReportDetail {
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

public class Deduct
{
    public string id {get; set;} = string.Empty;
    public string cashbasis_total {get; set;} = string.Empty;
    public string tru_kyquy_total {get; set;} = string.Empty;
    public string tru_vipham_bienban_total {get; set;} = string.Empty;
    public string tru_phidaudo_total {get; set;} = string.Empty;
    public string tru_tienquatram_total {get; set;} = string.Empty;
    public string tru_tamung_total {get; set;} = string.Empty;
    public string tru_khac_total {get; set;} = string.Empty;
    public int records {get; set;}
    public string createdAt {get; set;} = string.Empty;
    public List<DeductDetail>? cashbasisDetail {get; set;} 
}

// Chi tiết các khoản trừ của lái xe
public class DeductDetail {
    public string id {get; set;} = string.Empty;
    public string id_tongdoanhthu {get; set;} = string.Empty;
    public string nhan_vien_tao {get; set;} = string.Empty;
    public string ngay_tao {get; set;} = string.Empty;
    public string thang_nam {get; set;} = string.Empty;
    public string so_tai {get; set;} = string.Empty;
    public string msnv {get; set;} = string.Empty;
    public string hoten_laixe {get; set;} = string.Empty;
    public string hoten_msnv {get; set;} = string.Empty;
    public string tru_kyquy {get; set;} = string.Empty;
    public string tru_tainan {get; set;} = string.Empty;
    public string tru_luongung {get; set;} = string.Empty;
    public string tru_vipham_bienban {get; set;} = string.Empty;
    public string tru_bhxh {get; set;} = string.Empty;
    public string tru_tncn {get; set;} = string.Empty;
    public string tru_trachnhiem_baoquan_xe {get; set;} = string.Empty;
    public string tru__trachnhiem_loi_dongphuc {get; set;} = string.Empty;
    public string tru_trachnhiem_giaoca {get; set;} = string.Empty;
    public string tru_phidaudo {get; set;} = string.Empty;
    public string tru_khac {get; set;} = string.Empty;
    public string ghichu_trukhac {get; set;} = string.Empty;
    public string ghi_chu {get; set;} = string.Empty;
    public string show_thongtinchung {get; set;} = string.Empty;
    public string show_cackhoantru {get; set;} = string.Empty;
    public string show_trutien_trachnhiem {get; set;} = string.Empty;
    public string tru_tienquatram {get; set;} = string.Empty;
    public string tru_phisac {get; set;} = string.Empty;
    public string tru_tienkhoanxe {get; set;} = string.Empty;
    public string tru_tamung {get; set;} = string.Empty;
    																		

    // Tính tổng các cột
    public string _SumAll => SumString.SumFields(new List<string> {
                                                        tru_kyquy,
                                                        tru_tainan,
                                                        tru_luongung,
                                                        tru_vipham_bienban,
                                                        tru_bhxh,
                                                        tru_tncn,
                                                        tru_trachnhiem_baoquan_xe,
                                                        tru__trachnhiem_loi_dongphuc,
                                                        tru_trachnhiem_giaoca,
                                                        tru_phidaudo,
                                                        tru_khac,
                                                        tru_tienquatram,
                                                        tru_tamung,
                                                        tru_phisac,
                                                        tru_tienkhoanxe
                                        });

}