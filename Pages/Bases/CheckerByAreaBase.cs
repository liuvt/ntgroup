using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using ntgroup.Extensions;

namespace ntgroup.Pages.Bases;
public class CheckerByAreaBase : ComponentBase
{
    [Inject]
    protected ISpreadsConfigService spreadsConfigService { get; set; }
    [Inject]
    protected ISpreadsMainService spreadsMainService { get; set; }
    [Inject]
    protected NavigationManager NavigationManager { get; set; }
    protected IEnumerable<Area> areas { get; set; } = new List<Area>();
    protected Area area { get; set; } = new Area();
    protected string _searchString {get; set;} = string.Empty;

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
    protected async Task navigationTo()
    {
        NavigationManager.NavigateTo($"/checkers/{area.area_Id}/{_searchString}");
    }


}