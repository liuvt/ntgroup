using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using ntgroup.Extensions;

namespace ntgroup.Pages.Bases;
public class CheckerDetailBase : ComponentBase
{
    [Parameter]
    public string area_Id { get; set; }
    [Parameter]
    public string numberCar { get; set; }
    [Inject]
    protected ISpreadsConfigService spreadsConfigService { get; set; }
    [Inject]
    protected ISpreadsMainService spreadsMainService { get; set; }
    protected IEnumerable<Area> areas { get; set; } = new List<Area>();
    protected Area area { get; set; } = new Area();
    protected IEnumerable<Banking> bankings { get; set; } = new List<Banking>();
    protected Banking banking { get; set; } = new Banking();


    protected List<BillContract>? servicedataContracts = new List<BillContract>();
    protected List<BillTimepiece> servicedataTimepieces = new List<BillTimepiece>();
    protected BillShiftwork servicedataShiftwork = new BillShiftwork();
    public string totalPriceContract;
    public string totalPriceTimepiece;
    public string totalAmount;
    public string totalWallet;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            areas = await this.listAreas();
            area = areas.Where(e => e.area_Id == area_Id).FirstOrDefault();
            bankings = await this.listBankings();
            banking = bankings.Where(e => e.bank_Id == area.bank_Id).FirstOrDefault();

            servicedataContracts = await spreadsMainService.GetContractsByNumberCar(numberCar,area.area_SpreadId);
            servicedataTimepieces = await spreadsMainService.GetTimepiecesByNumberCar(numberCar,area.area_SpreadId);
            servicedataShiftwork = await spreadsMainService.GetShiftworksByNumberCar(numberCar,area.area_SpreadId,banking); // QR Kiên Giang
            if(area_Id.ToUpper() == "BL")
            {
                totalWallet = await spreadsMainService.TotalWalletGSMByNumberCar(numberCar,area.area_SpreadId);
            }else{
                totalWallet = "0";
            }
            
            totalPriceContract = SumString.SumListString(servicedataContracts.Cast<object>().ToList(), "TotalPrice");
            totalPriceTimepiece = SumString.SumListString(servicedataTimepieces.Cast<object>().ToList(), "Amount");
            totalAmount = SumString.SumDoubleString(totalPriceContract,totalPriceTimepiece);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<List<Area>> listAreas() => await this.spreadsConfigService.GetAreas();
    private async Task<List<Banking>> listBankings() => await this.spreadsConfigService.GetBankings();

}