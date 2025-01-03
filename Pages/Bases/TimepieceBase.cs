using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Services.Interfaces;
using MudBlazor;
using ntgroup.ErrorMessage;
using ntgroup.Data.Entities;

namespace ntgroup.Pages.Bases;
public class TimepieceBase : ComponentBase
{
    [Inject]
    protected ITimepieceService timepieceService {get; set;}
    [Inject]
    private ISnackbar snackBar { get; set; }
    protected IEnumerable<TimepieceDTO> Elements = new List<TimepieceDTO>();
    protected string _searchString; //Tìm kiếm dữ liệu
    //Biến kiểm tra đã click vào Xem lưới dữ liệu chưa
    protected bool isLoadGird = true;
    protected override async Task OnInitializedAsync()
    {
        isLoadGird = false; //Chưa click xem data
    }

    // Thanh search tìm kiếm
    protected Func<TimepieceDTO, bool> _quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (x.taxi_NumberId.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.taxi_NumberPlate.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.tp_StartPoint.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if ($"{x.taxi_NumberId}".Contains(_searchString))
            return true;

        return false;
    };
    
    protected async Task loadFileExcels(IBrowserFile file)
    {
        try
        {   
            isLoadGird = true; //Đã click vào xem

            //Kiểm tra format có đúng tên file chưa BC_DONGHO.xlsx
            if(file.Name != ExcelNotification.TIMEPIECE_FILE_NAME)
            {
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            //Lấy dữ liệu từ file excel
            Elements = await timepieceService.GetsByExcel(file); 
            StateHasChanged();
        }
        catch(Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
            Console.WriteLine(ex.Message);
        }
    }
}