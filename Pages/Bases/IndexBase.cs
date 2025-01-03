using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using ntgroup.Extensions;

namespace ntgroup.Pages.Bases;
public class IndexBase : ComponentBase
{

    protected string searchString1 = "";
    protected DemoDataSkySoftMain selectedItem1 = null;

    protected List<DemoDataSkySoftMain> dataSkySoft = new List<DemoDataSkySoftMain>();

    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string ApplicationName = "ntgroupforchecker";
    private readonly string Spreadsheets = "1WW022rLTmzI499efQxYXEl4oHBMRLBNuGpdtSOyP8vk";
    private readonly string sheetSKYSOFT = "SKYSOFT";
    private readonly string sheetDANHSACHLENCA = "DANH SÁCH LÊN CA";
    //Tổng tiền skysoft
    protected string totalSkySoft;

    protected SheetsService sheetsService;

    protected override async Task OnInitializedAsync()
    {
        
        //File xác thực google thông qua file ntgroup-48f65f9c97d0.json
        GoogleCredential credential;
        using (var stream = new FileStream("ntgroup-48f65f9c97d0.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        dataSkySoft = await ReadDataSKYSOFT();
    }

    //Tìm kiếm
    protected bool FilterFunc1(DemoDataSkySoftMain element) => FilterFunc(element, searchString1);
    protected bool FilterFunc(DemoDataSkySoftMain element, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (element.NumberCar.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.NumberPlate.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (element.Static.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }
    
    //Dữ liệu từ Google Sheet SKYSOFT
    protected async Task<List<DemoDataSkySoftMain>> ReadDataSKYSOFT()
    {
        var datas = new List<DemoDataSkySoftMain>();
        var range = $"{sheetSKYSOFT}!A3:J";
        var request = sheetsService.Spreadsheets.Values.Get(Spreadsheets, range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                datas.Add(new DemoDataSkySoftMain
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    NumberPlate = item[1].ToString() ?? string.Empty,
                    Price = FormatCurrency.formatCurrency(item[6].ToString()),
                    Static = ((item[9].ToString() == "#REF!")) ? "Không kinh doanh" : item[9].ToString(),
                });
            }

            // Lấy tổng giá trị của tất cả các cuốc lẻ
            var totalList = datas.Sum(e => {
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
            totalSkySoft = FormatCurrency.formatCurrency(totalList.ToString());
        }
        else
        {
            Console.WriteLine("No data found.");
        }
        return datas;
    }

}