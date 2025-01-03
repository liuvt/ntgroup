using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using ntgroup.Extensions;

namespace ntgroup.Pages.Bases;
public class CheckOutBase : ComponentBase
{
    [Parameter]
    public string numberCar { get; set; }
    [Inject]
    protected ISheetContractService sheetContractService {get; set;}
    [Inject]
    protected ISheetTimepieceService sheetTimepieceService {get; set;}

    protected List<BillContract> servicedataContracts = new List<BillContract>();
    protected List<BillTimepiece> servicedataTimepieces = new List<BillTimepiece>();
    protected DemoDataShiftwork dataShiftworks = new DemoDataShiftwork();

    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string ApplicationName = "ntgroupforchecker";
    private readonly string Spreadsheets = "1WW022rLTmzI499efQxYXEl4oHBMRLBNuGpdtSOyP8vk";
    private readonly string sheetDANHSACHLENCA = "DANHSACHLENCA";
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
        servicedataContracts = await sheetContractService.Gets(numberCar);
        servicedataTimepieces = await sheetTimepieceService.Gets(numberCar);
        totalWallet = "-" + await ReadDataWALLETGSM(numberCar);
        
        totalPriceContract = SumTotalListString.SumTotalPrices(servicedataContracts.Cast<object>().ToList(), "TotalPrice");
        totalPriceTimepiece = SumTotalListString.SumTotalPrices(servicedataTimepieces.Cast<object>().ToList(), "Amount");

        totalAmount = FormatCurrency.formatCurrency((decimal.Parse(totalPriceContract) + decimal.Parse(totalPriceTimepiece)).ToString());
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
                    RevenueTotal = FormatCurrency.formatCurrency(item[2].ToString()),
                    RevenueByDate = FormatCurrency.formatCurrency(item[3].ToString()),
                    QRContext = item[4].ToString() ?? string.Empty,
                    QRUrl = item[5].ToString() ?? string.Empty,
                    TotalPrice = FormatCurrency.formatCurrency(item[6].ToString())
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