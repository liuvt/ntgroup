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
using DocumentFormat.OpenXml.Office.CustomUI;

namespace ntgroup.Pages.Bases;
public class IndexBase : ComponentBase
{

    [Inject]
    protected ISpreadsRegisterContractService sheetRegisterContractService { get; set; }
    protected IEnumerable<DefaultContract> defaultContracts = new List<DefaultContract>();
    protected string _searchString {get; set;} = string.Empty; //TextFields
    protected string stringValue { get; set; } = string.Empty; // TextSelect
    protected string stringResult { get; set; } = string.Empty; // Kết quả trả về khi không tìm thấy đối tượng
    protected override async Task OnInitializedAsync()
    {
        defaultContracts = await this.listOptions();
    }

    private async Task<List<DefaultContract>> listOptions()
    {
        var lists = new List<DefaultContract>();
        var listOptions = await sheetRegisterContractService.Gets();
        foreach(var item in listOptions)
        {

            if ((Convert.ToInt32(item.dc_DistanceOne) % 10) == 0)
            {
                lists.Add(item);
            }
        }
        return lists;
    }
}   