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

namespace ntgroup.APIs;

public class SpreadsReportServer : ISpreadsReportServer
{
    // Lấy câu hình xác thực tài khoản Google
    protected readonly IConfiguration configuration;

    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

    // Lấy SpreadSheetId để đỗ dữ liệu
    private readonly string spreadSheetId = "1Q642xyAmlz2qoxAszCJsOJTLxrcBlefpIFLJSJvAdoI";
    private readonly string sheetDOANH_THU_LX_NGAY_DIEN = "DOANH_THU_LX_NGAY_DIEN";
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

    // Lấy toàn bộ thông tin doanh thu lái xe mỗi ngày trong tháng
    public async Task<List<StatisticalReport>> Gets()
    {
        try
        {
            var statisticalReport = new List<StatisticalReport>();
            var range = $"{sheetDOANH_THU_LX_NGAY_DIEN}!A2:AP";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    statisticalReport.Add(new StatisticalReport
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

    public async Task<StatisticalReportTotal> GetsTotal()
    {
        try
        {
            var newObject = await this.Gets();
            if (newObject == null || newObject.Count <= 0)
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new StatisticalReportTotal
            {
                id = Guid.NewGuid().ToString(),
                cashbasis = SumString.SumListString<StatisticalReport>(newObject, "tien_phai_thu"),
                revenue = SumString.SumListString<StatisticalReport>(newObject, "doanh_thu"),
                records = newObject.Count,
                createdAt = newObject.Select(a => a.thang_nam).FirstOrDefault() ?? string.Empty,
                statisticalReports = newObject
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    public async Task<StatisticalReportTotal> GetsTotalbyMonth(string month)
    {
        try
        {
            var newObject = await this.Gets();
            var formatbyDate = month.Replace("%2F", "/");

            var result = newObject.Where(a => a.thang_nam == formatbyDate).ToList();

            if (result == null || result.Count <= 0)
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new StatisticalReportTotal
            {
                id = Guid.NewGuid().ToString(),
                cashbasis = SumString.SumListString<StatisticalReport>(result, "tien_phai_thu"),
                revenue = SumString.SumListString<StatisticalReport>(result, "doanh_thu"),
                records = result.Count,
                createdAt = formatbyDate,
                statisticalReports = result
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    // Select by id and Object
    public async Task<StatisticalReport> Get(string id)
    {
        try
        {
            var newObject = await this.Gets();
            var byId = newObject.Where(a => a.id == id).FirstOrDefault()!;

            if (byId == null)
            {
                throw new Exception($"ID ({id}) không tồn tại!");
            }


            return byId;

        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    // Lấy danh sách các ngày của 1 tài xế thông qua userId 
    public async Task<List<StatisticalReport>> GetsByUserId(string userId)
    {
        try
        {
            var newObject = await this.Gets();
            var listByUserId = newObject.Where(a => a.msnv == userId).ToList();

            if (listByUserId == null || listByUserId.Count <= 0)
            {
                throw new Exception($"Không tồn tại!");
            }

            return listByUserId;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }
    
    // Lấy danh sách các ngày của 1 tài xế thông qua userId 
    public async Task<List<StatisticalReport>> GetsYesterday()
    {
        try
        {
            var newObject = await this.Gets();
            var yesterday = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            var result = newObject.Where(a => a.thoi_gian_tao == yesterday).ToList();

            if (result == null || result.Count <= 0)
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

    public async Task<StatisticalReportTotal> GetsYesterdayTotal()
    {
        try
        {
            var newObject = await this.Gets();
            var yesterday = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            var result = newObject.Where(a => a.thoi_gian_tao == yesterday).ToList();

            if (result == null || result.Count <= 0)
            {
                throw new Exception($"Không tồn tại!");
            }

            var total = new StatisticalReportTotal
            {
                id = Guid.NewGuid().ToString(),
                cashbasis = SumString.SumListString<StatisticalReport>(result, "tien_phai_thu"),
                revenue = SumString.SumListString<StatisticalReport>(result, "doanh_thu"),
                records = result.Count,
                createdAt = yesterday,
                statisticalReports = result
            };

            return total;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    // Lấy danh sách các ngày của 1 tài xế thông qua userId 
    public async Task<List<StatisticalReport>> GetsByDay(string byDate)
    {
        try
        {
            var newObject = await this.Gets();
            var formatbyDate = byDate.Replace("%2F", "/");

            var result = newObject.Where(a => a.thoi_gian_tao == formatbyDate).ToList();

            if (result == null || result.Count <= 0)
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
}