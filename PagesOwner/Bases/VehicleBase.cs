using Microsoft.AspNetCore.Components;
using MudBlazor;
using ntgroup.Data.Models.Skysofts;
using ntgroup.Services;

namespace ntgroup.PagesOwner.Bases;
public class VehicleBase : ComponentBase
{
    [Inject]
    private NavigationManager nav { get; set; }
    [Inject]
    private ISnackbar snackBar { get; set; }
    [Inject]
    private ISkysoftService VehicleService { get; set; }

    protected IEnumerable<Vehicle> vehicles { get; set; }  = new List<Vehicle>();
    protected Vehicle vehicle { get; set; } = new Vehicle();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadVehicles();
        }
        catch (Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
        }
    }

    private async Task LoadVehicles()
    {
        try
        {
            vehicles = await VehicleService.GetsVehicles();
            snackBar.Add("Tải danh sách xe thành công!", Severity.Success);
        }
        catch (Exception ex)
        {
            snackBar.Add("Không thể tải dữ liệu: " + ex.Message, Severity.Error);
        }
    }
}