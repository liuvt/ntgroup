using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;

namespace ntgroup.Pages.Bases;
public class SearchContractBase : ComponentBase
{

    [Inject]
    protected ISpreadsRegisterContractService sheetRegisterContractService { get; set; }
    protected IEnumerable<DefaultContract> defaultContracts = new List<DefaultContract>();
    private IEnumerable<DefaultContract> Items = new List<DefaultContract>(); // Lấy dữ liệu về để tìm kiếm cục bộ. Lấy 1 lần đầu tiên khi load dữ liệu
    protected string _searchString { get; set; } = string.Empty; //TextFields
    protected string stringValue { get; set; } = string.Empty; // TextSelect
    protected string stringResult { get; set; } = string.Empty; // Kết quả trả về khi không tìm thấy đối tượng
    protected override async Task OnInitializedAsync()
    {
        Items = await sheetRegisterContractService.Gets(); // Load dữ liệu 1 lần 
        defaultContracts = await this.listOptions();
    }

    //Lọc lại dữ liệu
    private async Task<List<DefaultContract>> listOptions()
    {
        var lists = new List<DefaultContract>();
        foreach (var item in Items)
        {

            if ((Convert.ToInt32(item.dc_DistanceOne) % 10) == 0)
            {
                lists.Add(item);
            }
        }
        return lists;
    }

    protected async Task HandleTextChanged(string _searchString)
    {
        var results = new List<DefaultContract>();
        foreach (var item in Items)
        {
            if (
                (item.dc_DistanceOne == _searchString && stringValue == "1") ||
                (item.dc_DistanceOne == _searchString && string.IsNullOrWhiteSpace(stringValue))
            ) // Tìm kiếm 1 chiều và số km
            {
                results.Add(item);
            }

            if (item.dc_DistanceTwo == _searchString && stringValue == "2") // Tìm kiếm 2 chiều và số km
            {
                results.Add(item);
            }
        }
        if (
            (!string.IsNullOrWhiteSpace(_searchString) && string.IsNullOrWhiteSpace(stringValue)) ||
            (!string.IsNullOrWhiteSpace(_searchString) && !string.IsNullOrWhiteSpace(stringValue))
        )
        {
            defaultContracts = results;
        }
        else
        {
            Thread.Sleep(500);
            defaultContracts = await this.listOptions();
        }
        StateHasChanged();
    }

    protected async Task HandleTextSelectChanged(string stringValue)
    {
        var results = new List<DefaultContract>();
        foreach (var item in Items)
        {
            if (
                (item.dc_DistanceOne == _searchString && stringValue == "1") ||
                (item.dc_DistanceOne == _searchString && string.IsNullOrWhiteSpace(stringValue))
            ) // Tìm kiếm 1 chiều và số km
            {
                results.Add(item);
            }

            if (item.dc_DistanceTwo == _searchString && stringValue == "2") // Tìm kiếm 2 chiều và số km
            {
                results.Add(item);
            }
        }

        if (
            (!string.IsNullOrWhiteSpace(_searchString) && string.IsNullOrWhiteSpace(stringValue)) ||
            (!string.IsNullOrWhiteSpace(_searchString) && !string.IsNullOrWhiteSpace(stringValue))
        )
        {
            Thread.Sleep(500);
            defaultContracts = results;
        }
        else
        {
            defaultContracts = await this.listOptions();
        }
        StateHasChanged();
    }
}