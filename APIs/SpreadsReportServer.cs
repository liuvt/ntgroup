using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using System.Globalization;
using ntgroup.Extensions;
using System.Collections.Frozen;

namespace ntgroup.APIs;

public class SpreadsReportServer : ISpreadsReportServer
{
    // Lấy câu hình xác thực tài khoản Google
    protected readonly IConfiguration configuration;

    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

    // Lấy SpreadSheetId để đỗ dữ liệu
    private readonly string spreadSheetId = "1Q642xyAmlz2qoxAszCJsOJTLxrcBlefpIFLJSJvAdoI";
    private readonly string sheetDOANH_THU_LX_NGAY_DIEN = "DOANH_THU_LX_NGAY_DIEN";
    private readonly string sheetCAC_KHOAN_TRU_LUONG_LX_DIEN = "CAC_KHOAN_TRU_LUONG_LX_DIEN";

    private SheetsService sheetsService;

    // Constructor
    public SpreadsReportServer(IConfiguration _configuration)
    {
        this.configuration = _configuration;

        //File xác thực google tài khoản
        GoogleCredential credential;
        using (var stream = new FileStream(configuration["GoogleSheetConfig:ServiceAccount"]!, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        // Đăng ký service
        sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = configuration["GoogleSheetConfig:ApplicationName"],
        });

    }

    // Lấy danh sách chi tiết
    public async Task<List<StatisticalReportDetail>> GetsStatisticalReportDetail()
    {
        try
        {
            var statisticalReport = new List<StatisticalReportDetail>();
            var range = $"{sheetDOANH_THU_LX_NGAY_DIEN}!A2:AP";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    statisticalReport.Add(new StatisticalReportDetail
                    {
                        id = item[0].ToString() ?? string.Empty,
                        stt = item[1].ToString() ?? string.Empty,
                        thoi_gian_tao = item[2].ToString() ?? string.Empty,
                        thang_nam = item[3].ToString() ?? string.Empty,
                        bks_sotai = item[4].ToString() ?? string.Empty,
                        msnv = item[5].ToString() ?? string.Empty,
                        hoten_laixe = item[6].ToString() ?? string.Empty,
                        hoten_msnv = item[7].ToString() ?? string.Empty,
                        doanh_thu = item[8].ToString() ?? string.Empty,
                        so_cuoc = item[9].ToString() ?? string.Empty,
                        sokm_vandoanh = item[10].ToString() ?? string.Empty,
                        sokm_cokhach = item[11].ToString() ?? string.Empty,
                        tru_tien = item[12].ToString() ?? string.Empty,
                        phu_thu = item[13].ToString() ?? string.Empty,
                        songay_nhap_doanhthu = item[14].ToString() ?? string.Empty,
                        ghi_chu = item[15].ToString() ?? string.Empty,
                        km_taplo = item[16].ToString() ?? string.Empty,
                        tien_phai_thu = item[17].ToString() ?? string.Empty,
                        so_tai = item[18].ToString() ?? string.Empty,
                        phi_qua_tram = item[19].ToString() ?? string.Empty,
                        socuoc_hopdong = item[20].ToString() ?? string.Empty,
                        socuoc_gsm = item[21].ToString() ?? string.Empty,
                        loaihinh_hoptac = item[22].ToString() ?? string.Empty,
                        tru_tour = item[23].ToString() ?? string.Empty,
                        tru_cupco = item[24].ToString() ?? string.Empty,
                        tru_appkh = item[25].ToString() ?? string.Empty,
                        truythu_km = item[26].ToString() ?? string.Empty,
                        truythu_gsm = item[27].ToString() ?? string.Empty,
                        truythu_appkh = item[28].ToString() ?? string.Empty,
                        ve_vi_gsm = item[29].ToString() ?? string.Empty,
                        gsm_10_phantram = item[30].ToString() ?? string.Empty,
                        tru_cuocxe_qua5h = item[31].ToString() ?? string.Empty,
                        tru_km_gsm = item[32].ToString() ?? string.Empty,
                        tru_km_appkh = item[33].ToString() ?? string.Empty,
                        tru_khac = item[34].ToString() ?? string.Empty,
                        truy_thu_thuc_thu = item[35].ToString() ?? string.Empty,
                        truy_thu_gio_cho = item[36].ToString() ?? string.Empty,
                        truy_thu_khac = item[37].ToString() ?? string.Empty,
                        baixe = item[38].ToString() ?? string.Empty,
                        tru_chenhlech_gsm = item[39].ToString() ?? string.Empty,
                        hinhthuc_kinhdoanh = item[40].ToString() ?? string.Empty,
                        tratruoc_trasau = item[41].ToString() ?? string.Empty
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            return statisticalReport;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    // Lấy thông tin tổng theo tháng
    public async Task<StatisticalReport> GetsStatisticalReportByMonth(string month)
    {
        try
        {
            var newObject = await this.GetsStatisticalReportDetail();
            var formatbyDate = month.Replace("%2F", "/");

            var result = newObject.Where(a => a.thang_nam == formatbyDate).ToList();

            if (!result.Any())
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new StatisticalReport
            {
                id = Guid.NewGuid().ToString(),
                cashbasis = SumString.SumListString<StatisticalReportDetail>(result, "tien_phai_thu"),
                revenue = SumString.SumListString<StatisticalReportDetail>(result, "doanh_thu"),
                records = result.Count,
                createdAt = formatbyDate,
                statisticalReportDetails = result
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    // Lấy thông tin tổng tháng của 1 driver
    public async Task<StatisticalReport> GetsStatisticalReportByUserID(string month, string msnv)
    {
        try
        {
            var newObject = await this.GetsStatisticalReportDetail();

            var result = newObject.Where(a => a.thang_nam == month.Replace("%2F", "/") && a.msnv == msnv).ToList();

            if (!result.Any())
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new StatisticalReport
            {
                id = Guid.NewGuid().ToString(),
                cashbasis = SumString.SumListString<StatisticalReportDetail>(result, "tien_phai_thu"),
                revenue = SumString.SumListString<StatisticalReportDetail>(result, "doanh_thu"),
                records = result.Count,
                createdAt = month.Replace("%2F", "/"),
                statisticalReportDetails = result
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu.", ex);
        }
    }

    #region 2. Các khoản trừ Deduct
    public async Task<List<DeductDetail>> GetsDeductDetails()
    {
        try
        {
            var cashbasisDetail = new List<DeductDetail>();
            var range = $"{sheetCAC_KHOAN_TRU_LUONG_LX_DIEN}!A2:AC";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values == null || values.Count == 0)
            {
                throw new Exception("Không có dữ liệu sheet.");
            }
            foreach (var item in values)
            {
                cashbasisDetail.Add(new DeductDetail
                {
                    id = item[0].ToString() ?? string.Empty,
                    id_tongdoanhthu = item[1].ToString() ?? string.Empty,
                    nhan_vien_tao = item[2].ToString() ?? string.Empty,
                    ngay_tao = item[3].ToString() ?? string.Empty,
                    thang_nam = item[4].ToString() ?? string.Empty,
                    so_tai = item[5].ToString() ?? string.Empty,
                    msnv = item[6].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    hoten_laixe = item[7].ToString() ?? string.Empty,
                    hoten_msnv = item[8].ToString() ?? string.Empty,
                    tru_kyquy = item[9].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_tainan = item[10].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_luongung = item[11].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_vipham_bienban = item[12].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_bhxh = item[13].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_tncn = item[14].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_trachnhiem_baoquan_xe = item[15].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru__trachnhiem_loi_dongphuc = item[16].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_trachnhiem_giaoca = item[17].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_phidaudo = item[18].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_khac = item[19].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    ghichu_trukhac = item[20].ToString() ?? string.Empty,
                    ghi_chu = item[21].ToString() ?? string.Empty,
                    show_thongtinchung = item[22].ToString() ?? string.Empty,
                    show_cackhoantru = item[23].ToString() ?? string.Empty,
                    show_trutien_trachnhiem = item[24].ToString() ?? string.Empty,
                    tru_tienquatram = item[25].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_phisac = item[26].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_tienkhoanxe = item[27].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty,
                    tru_tamung = item[28].ToString().Replace(".", "").Replace(",", "").Trim() ?? string.Empty
                });
            }

            return cashbasisDetail;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    // Lấy các khoản trừ theo mã nhân viên
    public async Task<DeductDetail> GetDeductDetail(string _manv)
    {
        try
        {
            var newObject = await GetsDeductDetails();
            var result = newObject.Where(a => a.msnv == _manv).FirstOrDefault();
            if (result == null)
            {
                throw new Exception($"Không tồn tại!");
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    public async Task<Deduct> GetsDeduct()
    {
        try
        {
            var newObject = await GetsDeductDetails();

            if (!newObject.Any())
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new Deduct
            {
                id = Guid.NewGuid().ToString(),
                cashbasis_total = SumString.SumListString<DeductDetail>(newObject, "_SumAll"),
                tru_kyquy_total = SumString.SumListString<DeductDetail>(newObject, "tru_kyquy"),
                tru_vipham_bienban_total = SumString.SumListString<DeductDetail>(newObject, "tru_vipham_bienban"),
                tru_phidaudo_total = SumString.SumListString<DeductDetail>(newObject, "tru_phidaudo"),
                tru_tienquatram_total = SumString.SumListString<DeductDetail>(newObject, "tru_tienquatram"),
                tru_tamung_total = SumString.SumListString<DeductDetail>(newObject, "tru_tamung"),
                tru_khac_total = SumString.SumListString<DeductDetail>(newObject, "tru_khac"),
                records = newObject.Count,
                cashbasisDetail = newObject
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    public async Task<Deduct> GetsDeductByMonth(string month)
    {
        try
        {
            var newObject = await this.GetsDeductDetails();
            var formatbyDate = month.Replace("%2F", "/");

            var result = newObject.Where(a => a.thang_nam == formatbyDate).ToList();

            if (!result.Any())
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new Deduct
            {
                id = Guid.NewGuid().ToString(),
                cashbasis_total = SumString.SumListString<DeductDetail>(result, "_SumAll"),
                tru_kyquy_total = SumString.SumListString<DeductDetail>(result, "tru_kyquy"),
                tru_vipham_bienban_total = SumString.SumListString<DeductDetail>(result, "tru_vipham_bienban"),
                tru_phidaudo_total = SumString.SumListString<DeductDetail>(result, "tru_phidaudo"),
                tru_tienquatram_total = SumString.SumListString<DeductDetail>(result, "tru_tienquatram"),
                tru_tamung_total = SumString.SumListString<DeductDetail>(result, "tru_tamung"),
                tru_khac_total = SumString.SumListString<DeductDetail>(result, "tru_khac"),
                records = result.Count,
                cashbasisDetail = result
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    #endregion
}