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

    protected List<BillContract>? servicedataContracts = new List<BillContract>();
    protected List<BillTimepiece> servicedataTimepieces = new List<BillTimepiece>();
    protected BillShiftwork servicedataShiftwork = new BillShiftwork();

    //For Google Sheet
    public string totalPriceContract;
    public string totalPriceTimepiece;
    public string totalAmount;
    public string totalWallet;

    protected SheetsService sheetsService;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            servicedataContracts = await sheetContractService.Gets(numberCar);
            servicedataTimepieces = await sheetTimepieceService.Gets(numberCar);
            if(numberCar.ToUpper().Contains("BL"))
            {
                servicedataShiftwork = await sheetShiftworkService.Get(numberCar);// QR Bạc Liêu 
            }
            else if(numberCar.ToUpper().Contains("RG"))
            {
                servicedataShiftwork = await sheetShiftworkService.GetKG(numberCar); // QR Kiên Giang
            }else
            {
                servicedataShiftwork = null;
            }

            totalWallet = await sheetTimepieceService.TotalWalletGSMByNumberCar(numberCar);
            totalPriceContract = SumString.SumListString(servicedataContracts.Cast<object>().ToList(), "TotalPrice");
            totalPriceTimepiece = SumString.SumListString(servicedataTimepieces.Cast<object>().ToList(), "Amount");
            totalAmount = SumString.SumDoubleString(totalPriceContract,totalPriceTimepiece);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

}