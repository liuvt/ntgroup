using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using ntgroup.Extensions;
using ntgroup.Services;
using System.Security.Claims;

namespace ntgroup.Pages.Bases;
public class CheckDetailBase : ComponentBase
{
    [Parameter]
    public string numberCar { get; set; }
    [Inject]
    protected ISpreadsConfigService spreadsConfigService { get; set; }
    [Inject]
    protected IAuthenService authenService { get; set; }
    [Inject]
    protected ISpreadsCheckerService spreadsCheckerService { get; set; }
    protected IEnumerable<Area> areas { get; set; } = new List<Area>();
    protected Area area { get; set; } = new Area();
    protected IEnumerable<Banking> bankings { get; set; } = new List<Banking>();
    protected Banking banking { get; set; } = new Banking();

    protected List<ReportContract>? contracts = new List<ReportContract>();
    protected List<ReportTimepiece> timepieces = new List<ReportTimepiece>();
    protected ReportTotal shiftworks = new ReportTotal();
    public string totalPriceContract;
    public string totalPriceTimepiece;
    public string totalAmount;
    public string totalWallet;

    protected override async Task OnInitializedAsync()
    {
        try
        {   
            var user = await authenService.GetUserAuth();
            areas = await this.listAreas();
            area = areas.Where(e => e.area_Id == user.area_Id).FirstOrDefault();

            bankings = await this.listBankings();
            banking = bankings.Where(e => e.bank_Id == area.bank_Id).FirstOrDefault()!;

            // Thống tin sheet Hợp đồng
            contracts = await spreadsCheckerService.GetContractsByNumberCar(numberCar.ToUpper(),area.area_SpreadId);
            // Thông tin sheet Đồng hồ
            timepieces = await spreadsCheckerService.GetTimepiecesByNumberCar(numberCar.ToUpper(),area.area_SpreadId);
            // Thông tin Tổng và QR chuyển khoản
            shiftworks = await spreadsCheckerService.GetShiftworksByNumberCar(numberCar.ToUpper(),area.area_SpreadId,banking); // QR Kiên Giang
            
            // Tổng ví
            totalWallet = await spreadsCheckerService.TotalWalletGSMByNumberCar(numberCar.ToUpper(),area.area_SpreadId);
            
            // Tổng tiền hợp đồng
            totalPriceContract = SumString.SumListString(contracts.Cast<object>().ToList(), "TotalPrice");
            // Tổng tiền đồng hồ
            totalPriceTimepiece = SumString.SumListString(timepieces.Cast<object>().ToList(), "Amount");

            // Tổng tiền
            totalAmount = SumString.SumDoubleString(totalPriceContract,totalPriceTimepiece);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<List<Area>> listAreas() => await this.spreadsConfigService.GetAreas();
    private async Task<List<Banking>> listBankings() => await this.spreadsConfigService.GetBankings();

    //Lấy thông tin trên access-token
    private async Task GetUserId()
    {
        var authState = await authenService.GetAuthenState();
        var user = authState.User;
        if (user.Identity.IsAuthenticated)
        {
            Console.WriteLine(user.FindFirst("area")?.Value ?? "Không tìm thấy Khu vực");
            Console.WriteLine(user.FindFirst("id")?.Value ?? "Mã nhân viên");
            Console.WriteLine(user.FindFirst(ClaimTypes.Role)?.Value ?? "Không tìm thấy role");
        }
        else
        {
            throw new Exception("Vui lòng đăng nhập lại");
        }
    }

}