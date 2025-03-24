using Microsoft.AspNetCore.Components;
using MudBlazor;
using ntgroup.Data.Entities.Skysofts;
using ntgroup.Services;

namespace ntgroup.PagesOwner.Bases;
public class DashboardBase : ComponentBase
{
    [Inject]
    private NavigationManager nav { get; set; }
    [Inject]
    private ISnackbar snackBar { get; set; }
    [Inject]
    private ISkysoftService TripService { get; set; }

    protected IEnumerable<TripDTO> trips { get; set; }  = new List<TripDTO>();
    protected TripDTO trip { get; set; } = new TripDTO();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadTrips();
        }
        catch (Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
        }
    }

    private async Task LoadTrips()
    {
        try
        {
            var rq = new TripRequestDTO{
                DateReport = "20250321"
            };
            

            trips = await TripService.GetsTrips(rq);
            snackBar.Add("Tải danh sách cuốc xe thành công!", Severity.Success);
        }
        catch (Exception ex)
        {
            snackBar.Add("Không thể tải dữ liệu: " + ex.Message, Severity.Error);
        }
    }
}