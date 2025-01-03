using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SheetShiftworkService : ISheetShiftworkService
{
    private readonly IConfiguration configuration;
    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetDANHSACHLENCA = "DANHSACHLENCA";
    private SheetsService sheetsService;

    public SheetShiftworkService(IConfiguration _configuration)
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

    public async Task<BillShiftwork> Gets(string numberCar)
    {
        var sws = new List<BillShiftwork>();
        var range = $"{sheetDANHSACHLENCA}!A7:G";
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
                var url = @$"https://img.vietqr.io/image/970448-0869120210-print.png?amount={item[6].ToString()}&addInfo={item[4].ToString()}&accountName=TRUONG%20TRUNG%20TIEP";
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

        var shiftworkByNumberCar = sws.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).FirstOrDefault();
        if(shiftworkByNumberCar == null)
        {
            shiftworkByNumberCar = new BillShiftwork();
        }

        return shiftworkByNumberCar;
    }

}
