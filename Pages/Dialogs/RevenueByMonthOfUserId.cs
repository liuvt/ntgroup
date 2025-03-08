using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using ntgroup.Services;
using ntgroup.Services.Interfaces;

namespace ntgroup.Pages.Dialogs;
public class RevenueByMonthOfUserIdBase : ComponentBase
{
    [CascadingParameter]
    protected IMudDialogInstance MudDialog { get; set; }
    [Inject] 
    private ISnackbar snackBar { get; set; }
    [Inject]
    protected ISpreadsReportService contextReports { get; set; }
    [Parameter]
    public string month { get; set; } = string.Empty;
    [Parameter]
    public string userId { get; set; } = string.Empty;
    protected CultureInfo culture = new CultureInfo("vi-VN");
    protected StatisticalReport statisticalReport { get; set; } = new StatisticalReport();
    protected IEnumerable<StatisticalReportDetail> statisticalReportDetail { get; set; } = new List<StatisticalReportDetail>(); // chi tiết
    protected bool _loading;

    protected void Ok() => MudDialog.Close(DialogResult.Ok(true));

    protected void Cancel() => MudDialog.Cancel(); // Ở đây không dùng
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await base.OnInitializedAsync();
            _loading = true;
            await this.GetStatisticalReport();
            _loading = false;

        }
        catch (Exception ex)
        {
            snackBar.Add(ex.Message, Severity.Error);
        }
    }

    // Gets data by month
    private async Task GetStatisticalReport()
    {
        try
        {
            // lấy thông tin doanh thu
            statisticalReport = await contextReports.GetsStatisticalReportByUserID(month, userId);

            // chi tiết doanh thu
            if (statisticalReport.statisticalReportDetails != null)
            {
                statisticalReportDetail = statisticalReport.statisticalReportDetails;
                snackBar.Add("Đã tải dữ liệu thành công", Severity.Success);
            };
        }
        catch (Exception ex)
        {
            snackBar.Add(ex.Message, Severity.Error);
        }
    }
}