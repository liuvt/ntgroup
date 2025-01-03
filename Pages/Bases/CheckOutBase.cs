using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using DocumentFormat.OpenXml.Office2013.Excel;
using System.Globalization;
using ntgroup.Extensions;
using System.Runtime.InteropServices;

namespace ntgroup.Pages.Bases;
public class CheckOutBase : ComponentBase
{
    [Parameter]
    public string numberCar { get; set; }

    protected List<DemoDataContract> dataContracts = new List<DemoDataContract>();
    protected DemoDataShiftwork dataShiftworks = new DemoDataShiftwork();
    protected List<DemoDataTimepiece> dataTimepieces = new List<DemoDataTimepiece>();

    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string ApplicationName = "ntgroupforchecker";
    private readonly string Spreadsheets = "1WW022rLTmzI499efQxYXEl4oHBMRLBNuGpdtSOyP8vk";
    private readonly string sheetDATALE = "DATALE";
    private readonly string sheetDANHSACHLENCA = "DANHSACHLENCA";
    private readonly string sheetDATAHOPDONG = "DATAHOPDONG";
    private readonly string sheetWALLETGSM = "VỀ VÍ GSM";
    public string totalPriceContract;
    public string totalPriceTimepiece;
    public string totalAmount;
    public string totalWallet;

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

        dataShiftworks = await ReadDataDANHSACHLENCA(numberCar);
        dataContracts = await ReadDataDATAHOPDONG(numberCar);
        dataTimepieces = await ReadDataDATALE(numberCar);
        totalWallet = "-" + await ReadDataWALLETGSM(numberCar);

        totalAmount = FormatCurrency.formatCurrency((decimal.Parse(totalPriceContract) + decimal.Parse(totalPriceTimepiece)).ToString(), "vi-VN");
    }

    private async Task<List<DemoDataTimepiece>> ReadDataDATALE(string numberCar)
    {
        var tps = new List<DemoDataTimepiece>();
        var range = $"{sheetDATALE}!A2:H";
        var request = sheetsService.Spreadsheets.Values.Get(Spreadsheets, range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {
                tps.Add(new DemoDataTimepiece
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
        
        var getObject = tps.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        if(getObject.Count <= 0)
        {
            getObject.Add(new DemoDataTimepiece(){
                NumberCar = numberCar.ToUpper(),
                StartTime = "//",
                EndTime =  "//",
                Distance =  "//",
                Amount =  "//",
                PickUp =  "//",
                DropOut =  "//",
                Note =  "//"
            });
        }

        // Lấy tổng giá trị của tất cả các cuốc lẻ
        var totalList = getObject.Sum(e => {
                // Chuyển đổi Price từ string sang decimal
                if (decimal.TryParse(e.Amount, out decimal price))
                {
                    return price;
                }
                else
                {
                    return 0; // Nếu không chuyển đổi được, coi như giá là 0
                }
            });
        totalPriceTimepiece = FormatCurrency.formatCurrency(totalList.ToString());

        return getObject;
    }

    private async Task<List<DemoDataContract>> ReadDataDATAHOPDONG(string numberCar)
    {
        var cts = new List<DemoDataContract>();
        var range = $"{sheetDATAHOPDONG}!B2:I";
        var request = sheetsService.Spreadsheets.Values.Get(Spreadsheets, range);
        var response = await request.ExecuteAsync();
        var values = response.Values;
        if (values != null && values.Count > 0)
        {
            foreach (var item in values)
            {   
                // Nếu không có dữ liệu thì thoát
                if(item[0].ToString() == string.Empty)
                {
                    break;
                }

                cts.Add(new DemoDataContract
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

        var getObject = cts.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).ToList();
        // Nếu không có trả về 1 giá trị mặc định
        if(getObject.Count <= 0)
        {
            getObject.Add(new DemoDataContract(){
                NumberCar = numberCar.ToUpper(),
                Key = "//",
                Price =  "//",
                DefaultDistance =  "//",
                OverDistance =  "//",
                Surcharge =  "//",
                Promotion =  "//",
                TotalPrice =  "//"
            });
        }

        // Lấy tổng giá trị của tất cả các hợp đồng
        var totalList = getObject.Sum(e => {
                // Chuyển đổi Price từ string sang decimal
                if (decimal.TryParse(e.TotalPrice, out decimal price))
                {
                    return price;
                }
                else
                {
                    return 0; // Nếu không chuyển đổi được, coi như giá là 0
                }
            });

        totalPriceContract = FormatCurrency.formatCurrency(totalList.ToString());

        // Trả về danh sách hợp đồng
        return getObject;
    }

    protected async Task<DemoDataShiftwork> ReadDataDANHSACHLENCA(string numberCar)
    {
        var sws = new List<DemoDataShiftwork>();
        var range = $"{sheetDANHSACHLENCA}!A7:G";
        var request = sheetsService.Spreadsheets.Values.Get(Spreadsheets, range);
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

                sws.Add(new DemoDataShiftwork
                {
                    NumberCar = item[0].ToString() ?? string.Empty,
                    NumberDriver = item[1].ToString() ?? string.Empty,
                    RevenueTotal = FormatCurrency.formatCurrency(item[2].ToString(), "vi-VN"),
                    RevenueByDate = FormatCurrency.formatCurrency(item[3].ToString(), "vi-VN"),
                    QRContext = item[4].ToString() ?? string.Empty,
                    QRUrl = item[5].ToString() ?? string.Empty,
                    TotalPrice = FormatCurrency.formatCurrency(item[6].ToString(), "vi-VN")
                });
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }

        var getObject = sws.Select(e=> e).Where(e => e.NumberCar == numberCar.ToUpper()).FirstOrDefault();
        if(getObject == null)
        {
            getObject = new DemoDataShiftwork();
        }

        return getObject;
    }


    private async Task<string> ReadDataWALLETGSM(string numberCar)
    {
        var walletGSM = new List<DemoDataWalletGSM>();
        var range = $"{sheetWALLETGSM}!A2:B";
        var request = sheetsService.Spreadsheets.Values.Get(Spreadsheets, range);
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
                walletGSM.Add(new DemoDataWalletGSM
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