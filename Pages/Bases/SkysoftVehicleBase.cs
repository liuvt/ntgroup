using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models.Skysofts;
using ntgroup.Services.Interfaces;
using MudBlazor;
using ntgroup.Extensions;
using ntgroup.Services;
using System.Security.Claims;

namespace ntgroup.Pages.Bases;
public class SkysoftVehicleBase : ComponentBase
{
    [Inject]
    protected ISkysoftService skysoftVehicle { get; set; }
    protected List<Vehicle> vehicle = new List<Vehicle>();
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Console.WriteLine("1");
            vehicle = await skysoftVehicle.GetsVehicles();
            Console.WriteLine(vehicle.Count().ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}