using System.Globalization;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Pages.Dialogs;
using ntgroup.Services.Interfaces;

namespace ntgroup.Pages.Bases;
public class RevenueByMonthBase : ComponentBase
{
    [Inject]
    protected ISpreadsReportService contextReports { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }
    [Inject] private ISnackbar snackBar { get; set; }
    protected StatisticalReport statisticalReport = new StatisticalReport();
    protected IEnumerable<StatisticalReportDetailDTO> statisticalReportDetailDTO = new List<StatisticalReportDetailDTO>(); // chi tiết
    protected DateTime? _yearMonth = DateTime.Now;
    protected List<string> _months = new List<string>();
    protected CultureInfo culture = new CultureInfo("vi-VN");
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await this.GetStatisticalReport();
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
            var month = _yearMonth?.ToString("MM/yyyy", culture);
            // lấy thông tin doanh thu
            statisticalReport = await contextReports.GetsStatisticalReportByMonth(month);

            // chi tiết doanh thu
            if (statisticalReport.statisticalReportDetails != null)
            {
                var statisticalDetail = statisticalReport.statisticalReportDetails;

                // Group by Category and sum the values
                var data = statisticalReport.statisticalReportDetails
                    .GroupBy(i => i.msnv)
                    .Select(g => new
                    {
                        thang_nam = g.First().thang_nam,
                        bks_sotai = g.First().bks_sotai,
                        msnv = g.Key,
                        hoten_laixe = g.First().hoten_laixe,
                        doanh_thu = g.Sum(i => decimal.TryParse(i.doanh_thu, out decimal value) ? value : 0), // Convert safely
                        tien_phai_thu = g.Sum(i => decimal.TryParse(i.tien_phai_thu, out decimal value) ? value : 0), // Convert safely
                        so_tai = g.First().so_tai,
                        loaihinh_hoptac = g.First().loaihinh_hoptac
                    }).ToList();

                statisticalReportDetailDTO = data.Select(i => new StatisticalReportDetailDTO
                {
                    thang_nam = i.thang_nam,
                    bks_sotai = i.bks_sotai,
                    msnv = i.msnv,
                    hoten_laixe = i.hoten_laixe,
                    doanh_thu = i.doanh_thu,
                    tien_phai_thu = i.tien_phai_thu,
                    so_tai = i.so_tai,
                    loaihinh_hoptac = i.loaihinh_hoptac
                }).ToList();
                snackBar.Add("Đã tải dữ liệu thành công", Severity.Success);
            }
            ;

        }
        catch (Exception ex)
        {
            snackBar.Add(ex.Message, Severity.Error);
        }
    }

    // Event change date
    protected async Task OnDateChanged(DateTime? newDate)
    {
        try
        {
            _yearMonth = newDate;
            await this.GetStatisticalReport();
        }
        catch (Exception ex)
        {
            snackBar.Add(ex.Message, Severity.Error);
        }
    }

    // Dialog userID
    protected async Task OpenDialogAsync(string month, string userId)
    {
        var parameters = new DialogParameters<RevenueByMonthOfUserId>
        {
            { "month", month },
            { "userId", userId },
        };

        var options = new DialogOptions { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<RevenueByMonthOfUserId>("", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            // Refresh data
            await this.GetStatisticalReport();
            StateHasChanged();
        }
    }
}