using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SheetRegisterContractService : ISheetRegisterContractService
{
    private readonly IConfiguration configuration;
    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string SpreadsSheetID = "1i0ZV-0ZBUF0j5QWW1ag3boWHjFWM6XJ3Zn-NKriGeFo";
    private readonly string sheetTRA_CUU_GIA_HD = "TRA_CUU_GIA_HD";
    private SheetsService sheetsService;

    public SheetRegisterContractService(IConfiguration _configuration)
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
    public async Task<List<DefaultContract>> Gets()
    {
        var lists = new List<DefaultContract>();
        var range = $"{sheetTRA_CUU_GIA_HD}!A3:J";
        var request = sheetsService.Spreadsheets.Values.Get(SpreadsSheetID, range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                lists.Add(new DefaultContract
                {
                    dc_Id = item[0].ToString() ?? string.Empty,
                    dc_DistanceOne = item[1].ToString() ?? string.Empty,
                    dc_DistanceTwo = item[2].ToString() ?? string.Empty,
                    dc_PriceOneFor5 = item[3].ToString(),
                    dc_PriceTwoFor5 = item[4].ToString(),
                    dc_PriceOneFor7 = item[5].ToString(),
                    dc_PriceTwoFor7 = item[6].ToString(),
                    dc_Time = item[7].ToString() ?? string.Empty,
                    dc_TimeFor7 = item[8].ToString() ?? string.Empty,
                    dc_DecriptionFor4 = item[9].ToString() ?? string.Empty,
                });
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }

        // Trả về danh sách hợp đồng
        return lists;
    }

}
