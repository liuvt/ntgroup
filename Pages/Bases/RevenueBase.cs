using Microsoft.AspNetCore.Components;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;

namespace ntgroup.Pages.Bases;
public class RevenueBase : ComponentBase
{
    [Inject]
    protected ISpreadsReportService contextReports { get; set; }
    protected IEnumerable<StatisticalReport> statisticalDetail = new List<StatisticalReport>();
    protected StatisticalReportTotal statisticalTotal = new StatisticalReportTotal();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await this.GetStatisticalReport();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task GetStatisticalReport()
    {
        try
        {
            statisticalTotal = await contextReports.GetsTotal();

            if(statisticalTotal.statisticalReports != null)
                statisticalDetail = statisticalTotal.statisticalReports;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}   