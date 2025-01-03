using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using ntgroup.Repositories.Interfaces;
using MudBlazor;
using ntgroup.ErrorMessage;
using ntgroup.Data.Entities;

namespace ntgroup.Pages.Bases;
public class CarBase : ComponentBase
{
    [Inject]
    protected ICarRepository carRepository {get; set;}
    protected IEnumerable<CarDTO> Elements = new List<CarDTO>();
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Elements = await carRepository.Gets();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}