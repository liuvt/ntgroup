using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using ntgroup.Extensions;
using System.Collections.Frozen;

namespace ntgroup.Pages.Bases;
public class CheckerByAreaBase : ComponentBase
{
    [Inject]
    protected ISpreadsConfigService spreadsConfigService { get; set; }
    [Inject]
    protected ISpreadsCheckerService spreadsCheckerService { get; set; }
    [Inject]
    protected NavigationManager NavigationManager { get; set; }
    protected IEnumerable<Area> areas { get; set; } = new List<Area>();
    protected Area area { get; set; } = new Area();
    
    protected ReportTotal reportTotal { get; set; } = new ReportTotal();
    protected IEnumerable<ReportTotal> reportTotals { get; set; } = new List<ReportTotal>();

    protected string _searchString {get; set;} = string.Empty;
    protected string _stringValue {get; set;} = string.Empty;

    protected string ErrorMessage {get; set;} = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        try
        {
            areas = await this.listAreas();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<List<Area>> listAreas() => await this.spreadsConfigService.GetAreas();
    protected async Task GetShiftWork()
    {
        try
        {
            if(area != null)
            {
                reportTotal = await this.spreadsCheckerService.GetShiftworksByNumberCar(_searchString,area.area_SpreadId);
            }
        }
        catch(Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
    
    protected async Task HandleTextSelectChanged(Area _stringValue)
    {
        if(_stringValue!= null)
        {
            area = areas.Where(e => e.area_Id == _stringValue.area_Id.ToUpper()).FirstOrDefault()!;
        }

        StateHasChanged();
    }

    protected async Task HandleTextChanged(string _searchString)
    {
        reportTotal = new ReportTotal();
        ErrorMessage = string.Empty;
        await GetShiftWork();
        StateHasChanged();
    }

    protected async Task navigationTo(string numberCar)
    {
        NavigationManager.NavigateTo($"/checkers/{area.area_Id}/{numberCar}");
    }
}