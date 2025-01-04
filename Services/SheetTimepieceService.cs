using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SheetTimepieceService : ISheetTimepieceService
{
    private readonly IConfiguration configuration;
    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetDATALE = "DATALE";
    private readonly string sheetWALLETGSM = "VỀ VÍ GSM";
    private SheetsService sheetsService;

    public SheetTimepieceService(IConfiguration _configuration)
    {
        // Đọc file appsettings.json
        this.configuration = _configuration;

        //File xác thực google thông qua file ntgroup-48f65f9c97d0.json
        GoogleCredential credential;
        using (var stream = new FileStream(configuration["GoogleSheetService:FileNameConfig"], FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        // Đăng ký service
        sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = configuration["GoogleSheetService:ApplicationName"],
        });
    }

    public async Task<List<BillTimepiece>> Gets(string numberCar)
    {
        var tps = new List<BillTimepiece>();
        var range = $"{sheetDATALE}!A2:H";
        var request = sheetsService.Spreadsheets.Values.Get(configuration["GoogleSheetService:SpreadsSheetID"], range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
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
        
        var timepieceByNumberCar = tps.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        if(timepieceByNumberCar.Count <= 0)
        {
            timepieceByNumberCar.Add(new BillTimepiece());
        }

        return timepieceByNumberCar;
    }

    public async Task<string> TotalWalletGSMByNumberCar(string numberCar)
    {
        var walletGSM = new List<BillWalletGSM>();
        var range = $"{sheetWALLETGSM}!A2:B";
        var request = sheetsService.Spreadsheets.Values.Get(configuration["GoogleSheetService:SpreadsSheetID"], range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
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

}
