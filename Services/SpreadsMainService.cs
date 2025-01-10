using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SpreadsMainService : ISpreadsMainService
{
    private readonly IConfiguration configuration;
    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetDATAHOPDONG = "DATAHOPDONG";
    private readonly string sheetDATALE = "DATALE";
    private readonly string sheetWALLETGSM = "VỀ VÍ GSM";
    private readonly string sheetDANHSACHLENCA = "DANHSACHLENCA";
    private SheetsService sheetsService;

    public SpreadsMainService(IConfiguration _configuration)
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
    private async Task<List<BillContract>> GetContracts(string area_SpreadId)
    {
        var cts = new List<BillContract>();
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

                cts.Add(new BillContract
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
    public async Task<List<BillContract>> GetContractsByNumberCar(string numberCar, string area_SpreadId)
    {
        var cts = await this.GetContracts(area_SpreadId);
        //Lọc lại danh sách theo Mã Xe
        var byNumberCar = cts.Select(e => e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        // Nếu không có trả về 1 giá trị mặc định
        if (byNumberCar.Count <= 0)
        {
            byNumberCar.Add(new BillContract());
        }

        // Trả về danh sách hợp đồng
        return byNumberCar;
    }
    #endregion

    #region Timepieces
    // Lấy toàn bộ dữ liêu về: Theo khu vực
    private async Task<List<BillTimepiece>> GetTimepieces(string area_SpreadId)
    {
        var tps = new List<BillTimepiece>();
        var range = $"{sheetDATALE}!A2:H";
        var values = await this.APIGetValues(sheetsService, area_SpreadId, range);
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                tps.Add(new BillTimepiece
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
    public async Task<List<BillTimepiece>> GetTimepiecesByNumberCar(string numberCar, string area_SpreadId)
    {
        var tps = await this.GetTimepieces(area_SpreadId);

        var byNumberCar = tps.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        if(byNumberCar.Count <= 0)
        {
            byNumberCar.Add(new BillTimepiece());
        }

        return byNumberCar;
    }

    // Trừ ví
    public async Task<string> TotalWalletGSMByNumberCar(string numberCar, string area_SpreadId)
    {
        var walletGSM = new List<BillWalletGSM>();
        var range = $"{sheetWALLETGSM}!A2:B";
        var values = await this.APIGetValues(sheetsService, area_SpreadId, range);
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                if(item[0].ToString() == string.Empty)
                {
                    break;
                }

                walletGSM.Add(new BillWalletGSM
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    Price = FormatCurrency.formatCurrency(item[1].ToString()),
                });
            }
        }
        
        // Select lại 1 danh sách đối tượng có cùng NumberCar = numberCar
        var getObject = walletGSM.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        var totalWallet = "0";
        // Nếu không có trả về 1 giá trị mặc định
        if(getObject.Count > 0)
        {
           var getValueTotalWallet = getObject.Sum(e => {
                // Chuyển đổi Price từ string sang decimal
                if (decimal.TryParse(e.Price, out decimal price))
                {
                    return price;
                }
                else
                {
                    return 0; // Nếu không chuyển đổi được, coi như giá là 0
                }
            });

            totalWallet = getValueTotalWallet.ToString();
        }
        return FormatCurrency.formatCurrency(totalWallet.ToString());
    }
    #endregion

    #region Shiftworks
    private async Task<List<BillShiftwork>> GetShiftworks(string area_SpreadId, Banking banking)
    {
        var sws = new List<BillShiftwork>();
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
                sws.Add(new BillShiftwork
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

    public async Task<BillShiftwork> GetShiftworksByNumberCar(string numberCar, string area_SpreadId, Banking banking)
    {
        var sws = await this.GetShiftworks(area_SpreadId, banking);

        var shiftworkByNumberCar = sws.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).FirstOrDefault();
        if(shiftworkByNumberCar == null)
        {
            shiftworkByNumberCar = new BillShiftwork();
        }

        return shiftworkByNumberCar;
    }
    #endregion
}
