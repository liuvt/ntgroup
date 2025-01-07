using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SheetContractService : ISheetContractService
{
    private readonly IConfiguration configuration;
    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetDATAHOPDONG = "DATAHOPDONG";
    private SheetsService sheetsService;

    public SheetContractService(IConfiguration _configuration)
    {
        // Đọc file appsettings.json
        this.configuration = _configuration;

        //File xác thực google thông qua file ntgroup-48f65f9c97d0.json
        GoogleCredential credential;
        using (var stream = new FileStream("ntgroup-48f65f9c97d0.json", FileMode.Open, FileAccess.Read))
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
    
    // Bạc Liêu
    public async Task<List<BillContract>> Gets(string numberCar)
    {
        var cts = new List<BillContract>();
        var range = $"{sheetDATAHOPDONG}!B2:I";
        var request = sheetsService.Spreadsheets.Values.Get(configuration["GoogleSheetService:SpreadsSheetID"], range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
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

        //Add data Kiên Giang
        cts.AddRange(await this.GetsKG());

        //Lọc lại danh sách theo Mã Xe
        var getObject = cts.Select(e => e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        // Nếu không có trả về 1 giá trị mặc định
        if (getObject.Count <= 0)
        {
            getObject.Add(new BillContract());
        }

        // Trả về danh sách hợp đồng
        return getObject;
    }



    



    // Data Kiên Giang
    public async Task<List<BillContract>> GetsKG()
    {
        var cts = new List<BillContract>();
        var range = $"{sheetDATAHOPDONG}!B2:I";
        var request = sheetsService.Spreadsheets.Values.Get(configuration["GoogleSheetService:SpreadsSheetIDKG"], range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
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
        // Trả về danh sách hợp đồng
        return cts;
    }

}
