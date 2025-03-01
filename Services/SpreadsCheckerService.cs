using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SpreadsCheckerService : ISpreadsCheckerService
{
    private readonly IConfiguration configuration;
    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetDATAHOPDONG = "DATAHOPDONG";
    private readonly string sheetDATALE = "DATALE";
    private readonly string sheetDANHSACHLENCA = "DANHSACHLENCA";
    private SheetsService sheetsService;

    public SpreadsCheckerService(IConfiguration _configuration)
    {
       this.configuration = _configuration;

        //File xác thực google tài khoản
        GoogleCredential credential;
        using (var stream = new FileStream(configuration["GoogleSheetConfig:ServiceAccount"], FileMode.Open, FileAccess.Read))
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

    // Đỗ toàn bộ dữ liệu Sheet về để xữ lý
    private async Task<IList<IList<object>>> APIGetValues(SheetsService service, string spreadsheetId, string range)
    {
        var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var response = await request.ExecuteAsync();
        return response.Values;
    }

    #region Contracts
    // Lấy toàn bộ dữ liêu về: Theo khu vực
    private async Task<List<ReportContract>> GetContracts(string area_SpreadId)
    {
        var cts = new List<ReportContract>();
        var range = $"{sheetDATAHOPDONG}!B2:I";
        var values = await this.APIGetValues(sheetsService, area_SpreadId, range);
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                // Nếu không có dữ liệu thì thoát
                if (item[0].ToString() == string.Empty)
                {
                    break;
                }

                cts.Add(new ReportContract
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    Key = item[1].ToString() ?? string.Empty,
                    Price = FormatCurrency.formatCurrency(item[2].ToString()),
                    DefaultDistance = item[3].ToString() ?? string.Empty,
                    OverDistance = item[4].ToString() ?? string.Empty,
                    Surcharge = FormatCurrency.formatCurrency(item[5].ToString()),
                    Promotion = FormatCurrency.formatCurrency(item[6].ToString()),
                    TotalPrice = FormatCurrency.formatCurrency(item[7].ToString())
                });
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }

        return cts;
    }

    // Lấy danh sách theo mã nhân viên và khu vực
    public async Task<List<ReportContract>> GetContractsByNumberCar(string numberCar, string area_SpreadId)
    {
        var cts = await this.GetContracts(area_SpreadId);
        //Lọc lại danh sách theo Mã Xe
        var byNumberCar = cts.Select(e => e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        // Nếu không có trả về 1 giá trị mặc định
        if (byNumberCar.Count <= 0)
        {
            byNumberCar.Add(new ReportContract());
        }

        // Trả về danh sách hợp đồng
        return byNumberCar;
    }
    #endregion

    #region Timepieces
    // Lấy toàn bộ dữ liêu về: Theo khu vực
    private async Task<List<ReportTimepiece>> GetTimepieces(string area_SpreadId)
    {
        var tps = new List<ReportTimepiece>();
        var range = $"{sheetDATALE}!A2:H";
        var values = await this.APIGetValues(sheetsService, area_SpreadId, range);
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                tps.Add(new ReportTimepiece
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    StartTime = item[1].ToString() ?? string.Empty,
                    EndTime = item[2].ToString() ?? string.Empty,
                    Distance = item[3].ToString() ?? string.Empty,
                    Amount = FormatCurrency.formatCurrency(item[4].ToString()),
                    PickUp = item[5].ToString() ?? string.Empty,
                    DropOut = item[6].ToString() ?? string.Empty,
                    Note = (item.Count < 8) ? string.Empty : item[7].ToString()
                });
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }
        
        return tps;
    }

    // Lấy danh sách theo mã nhân viên và khu vực
    public async Task<List<ReportTimepiece>> GetTimepiecesByNumberCar(string numberCar, string area_SpreadId)
    {
        var tps = await this.GetTimepieces(area_SpreadId);

        var byNumberCar = tps.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        if(byNumberCar.Count <= 0)
        {
            byNumberCar.Add(new ReportTimepiece());
        }

        return byNumberCar;
    }

    #endregion

    #region ReportTotal
    // Tìm tài xế có lên ca
    private async Task<List<ReportTotal>> GetShiftworks(string area_SpreadId)
    {
        var sws = new List<ReportTotal>();
        var range = $"{sheetDANHSACHLENCA}!A7:G";
        var values = await this.APIGetValues(sheetsService, area_SpreadId, range);
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                if(item[0].ToString() == string.Empty)
                {
                    break;
                }
                sws.Add(new ReportTotal
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    NumberDriver = item[1].ToString() ?? string.Empty,
                    RevenueTotal = FormatCurrency.formatCurrency(item[2].ToString()!),
                    RevenueByDate = FormatCurrency.formatCurrency(item[3].ToString()!),
                    QRContext = item[4].ToString() ?? string.Empty,
                    QRUrl = item[4].ToString() ?? string.Empty,
                    TotalPrice = FormatCurrency.formatCurrency(item[6].ToString()!)
                });
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }

        return sws;
    }
    
    // Thông tin chuyển khoản phiếu checker, và thông tin tổng tiền, tổng doanh thu của tài xế
    private async Task<List<ReportTotal>> GetShiftworks(string area_SpreadId, Banking banking)
    {
        
        var sws = new List<ReportTotal>();
        var range = $"{sheetDANHSACHLENCA}!A7:G";
        var values = await this.APIGetValues(sheetsService, area_SpreadId, range);
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                if(item[0].ToString() == string.Empty)
                {
                    break;
                }
                var url = @$"{banking.bank_Url}{banking.bank_Id}-{banking.bank_Number}-{banking.bank_Type}?amount={item[6].ToString()}&addInfo={item[4].ToString()}&accountName={banking.bank_AccountName}";
                sws.Add(new ReportTotal
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    NumberDriver = item[1].ToString() ?? string.Empty,
                    RevenueTotal = FormatCurrency.formatCurrency(item[2].ToString()),
                    RevenueByDate = FormatCurrency.formatCurrency(item[3].ToString()),
                    QRContext = item[4].ToString() ?? string.Empty,
                    QRUrl = url,
                    TotalPrice = FormatCurrency.formatCurrency(item[6].ToString())
                });
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }

        return sws;
    }

    // Thông tin chuyển khoản phiếu checker, và thông tin tổng tiền, tổng doanh thu của tài xế
    public async Task<ReportTotal> GetShiftworksByNumberCar(string numberCar, string area_SpreadId, Banking banking)
    {
        var sws = await this.GetShiftworks(area_SpreadId, banking);

        var shiftworkByNumberCar = sws.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).FirstOrDefault();
        if(shiftworkByNumberCar == null)
        {
            shiftworkByNumberCar = new ReportTotal();
        }

        return shiftworkByNumberCar;
    }

    // Thông tin tài xế lên ca
    public async Task<ReportTotal> GetShiftworksByNumberCar(string numberCar, string area_SpreadId)
    {
        var sws = await this.GetShiftworks(area_SpreadId);

        var shiftworkByNumberCar = sws.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).FirstOrDefault();
        if(shiftworkByNumberCar == null)
        {
            throw new Exception ("Không tìm thấy");
        }

        return shiftworkByNumberCar;
    }
    #endregion

    #region Skysoft
   
    #endregion
}
