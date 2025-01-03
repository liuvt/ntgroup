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
    [Inject]
    protected ISheetShiftworkService sheetShiftworkService {get; set;}

    protected List<BillContract> servicedataContracts = new List<BillContract>();
    protected List<BillTimepiece> servicedataTimepieces = new List<BillTimepiece>();
    protected BillShiftwork servicedataShiftwork = new BillShiftwork();

    //For Google Sheet
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string ApplicationName = "ntgroupforchecker";
    private readonly string Spreadsheets = "1WW022rLTmzI499efQxYXEl4oHBMRLBNuGpdtSOyP8vk";

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

        servicedataShiftwork = await sheetShiftworkService.Gets(numberCar);
        servicedataContracts = await sheetContractService.Gets(numberCar);
        servicedataTimepieces = await sheetTimepieceService.Gets(numberCar);

        totalWallet = "-" + await sheetTimepieceService.TotalWalletGSMByNumberCar(numberCar);
        
        totalPriceContract = SumTotalListString.SumTotalPrices(servicedataContracts.Cast<object>().ToList(), "TotalPrice");
        totalPriceTimepiece = SumTotalListString.SumTotalPrices(servicedataTimepieces.Cast<object>().ToList(), "Amount");

        totalAmount = FormatCurrency.formatCurrency((decimal.Parse(totalPriceContract) + decimal.Parse(totalPriceTimepiece)).ToString());
    }

}