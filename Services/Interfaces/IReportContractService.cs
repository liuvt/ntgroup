using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface IReportContractService
{
    //Read File
    Task<List<ReportContract>> GetsByExcel(IBrowserFile _file);
} 
