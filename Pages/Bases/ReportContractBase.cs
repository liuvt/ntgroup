using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using ntgroup.ErrorMessage;

namespace ntgroup.Pages.Bases;
public class ReportContractBase : ComponentBase
{
    [Inject]
    protected IReportContractService reportContractService {get; set;}
    [Inject]
    private ISnackbar snackBar { get; set; }
    protected IEnumerable<ReportContract> Elements = new List<ReportContract>();
    protected string _searchString; //Tìm kiếm dữ liệu
    //Biến kiểm tra đã click vào Xem lưới dữ liệu chưa
    protected bool isLoadGird = true;
    protected override async Task OnInitializedAsync()
    {
        isLoadGird = false; //Chưa click xem data
    }


    // Thanh search tìm kiếm
    protected Func<ReportContract, bool> _quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (x.reportcontract_IdTaxi.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.reportcontract_TimeStart.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.reportcontract_PointStart.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if ($"{x.reportcontract_IdTaxi}".Contains(_searchString))
            return true;

        return false;
    };
    
    protected async Task loadFileExcels(IBrowserFile file)
    {
        try
        {
            isLoadGird = true;
            //Kiểm tra đúng template nếu không trả exception
            if(file.Name != ExcelNotification.REPORT_CONTRACTS_FILE_NAME)
            {
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            Elements = await reportContractService.GetsByExcel(file); //Load data từ file excel
            StateHasChanged();
        }
        catch(Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
            Console.WriteLine(ex.Message);
        }
    }
}