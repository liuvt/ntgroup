using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using ntgroup.ErrorMessage;

namespace ntgroup.Pages.Bases;
public class CustomerBase : ComponentBase
{
    [Inject]
    protected ICustomerService customerService {get; set;}
    [Inject]
    private ISnackbar snackBar { get; set; }
    protected IEnumerable<Customer> Elements = new List<Customer>();
    protected string _searchString; //Tìm kiếm dữ liệu
    protected string app_name = ExcelNotification.APP_FILE_NAME;
    protected string gsm_name = ExcelNotification.GSM_FILE_NAME;
    //Biến kiểm tra đã click vào Xem lưới dữ liệu chưa
    protected bool isLoadGird = true;
    protected override async Task OnInitializedAsync()
    {
        isLoadGird = false; //Chưa click xem data
    }

    // Thanh search tìm kiếm
    protected Func<Customer, bool> _quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (x.customer_Type.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.customer_Id.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.customer_IdTaxi.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.customer_Status.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if ($"{x.customer_Id}".Contains(_searchString))
            return true;

        return false;
    };
    
    protected async Task loadExcelFile(IBrowserFile file)
    {
        try
        {
            isLoadGird = true;
            throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
        }
        catch(Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
            Console.WriteLine(ex.Message);
        }
    }

    protected async Task loadExcelFileAppCustomer(IBrowserFile file)
    {
        try
        {
            isLoadGird = true;
            Elements = await customerService.GetsByExcel(file, "APP KHÁCH"); //Load data từ file excel
            StateHasChanged();
        }
        catch(Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
            Console.WriteLine(ex.Message);
        }
    }

    protected async Task loadExcelFileAppGSM(IBrowserFile file)
    {
        try
        {
            isLoadGird = true;
            Elements = await customerService.GetsByExcel(file, "APP GSM"); //Load data từ file excel
            StateHasChanged();
        }
        catch(Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
            Console.WriteLine(ex.Message);
        }
    }
}