using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface IReportTimelineService
{
    //Read File
    Task<List<ReportTimeline>> GetsByExcel(IBrowserFile _file);
} 
