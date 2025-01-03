using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using ntgroup.ErrorMessage;

namespace ntgroup.Pages.Bases;
public class ReportTimelineBase : ComponentBase
{
    [Inject]
    protected IReportTimelineService reportTimelineService {get; set;}
    [Inject]
    private ISnackbar snackBar { get; set; }
    protected IEnumerable<ReportTimeline> Elements = new List<ReportTimeline>();
    protected string _searchString; //Tìm kiếm dữ liệu
    //Biến kiểm tra đã click vào Xem lưới dữ liệu chưa
    protected bool isLoadGird = true;
    protected override async Task OnInitializedAsync()
    {
        isLoadGird = false; //Chưa click xem data
    }

    // Thanh search tìm kiếm
    protected Func<ReportTimeline, bool> _quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (x.rptimeline_IdTaxi.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.rptimeline_TimeStart.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (x.rptimeline_PointStart.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if ($"{x.rptimeline_IdTaxi}".Contains(_searchString))
            return true;

        return false;
    };
    
    protected async Task loadFileExcels(IBrowserFile file)
    {
        try
        {   
            isLoadGird = true;
            //Kiểm tra đúng template nếu không trả exception
            if(file.Name != ExcelNotification.REPORT_TIMELINE_FILE_NAME)
            {
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            Elements = await reportTimelineService.GetsByExcel(file); //Load data từ file excel
            StateHasChanged();
        }
        catch(Exception ex)
        {
            snackBar.Add("Lỗi: " + ex.Message, Severity.Error);
            Console.WriteLine(ex.Message);
        }
    }
}