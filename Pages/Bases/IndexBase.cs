using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using ntgroup.Extensions;

namespace ntgroup.Pages.Bases;
public class IndexBase : ComponentBase
{

    [Inject]
    protected ISheetRegisterContractService sheetRegisterContractService { get; set; }
    protected IEnumerable<DefaultContract> defaultContracts = new List<DefaultContract>();
    protected string _searchString {get; set;} = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        defaultContracts = await sheetRegisterContractService.Gets();
    }

    private async Task<List<DefaultContract>> ReSearchContains(string _searchString)
    {
        var results = new List<DefaultContract>();
        var Items = await sheetRegisterContractService.Gets();
        foreach(var item in Items)
        {
            if(
                item.dc_DistanceOne.Contains(_searchString, StringComparison.OrdinalIgnoreCase)||
                item.dc_DistanceTwo.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
            )
            {
                Console.WriteLine("Text: "+item.dc_DecriptionFor4);
                results.Add(item);
            }
        }

        return (string.IsNullOrWhiteSpace(_searchString)) ? Items : results;
    }

    protected async Task HandleTextChanged(string _searchString)
    {
        var results = new List<DefaultContract>();
        var Items = await sheetRegisterContractService.Gets();
        foreach(var item in Items)
        {
            if(
                item.dc_DistanceOne.Contains(_searchString, StringComparison.OrdinalIgnoreCase)||
                item.dc_DistanceTwo.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
            )
            {
                Console.WriteLine("Text: "+item.dc_DecriptionFor4);
                results.Add(item);
            }
        }

        defaultContracts = (string.IsNullOrWhiteSpace(_searchString)) ? Items : results;
    }

    
}