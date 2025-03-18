using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISpreadsReportService
{
    // Chi tiết doanh thu của lái xe
    Task<StatisticalReport> GetsStatisticalReportByMonth(string month);
    Task<StatisticalReport> GetsStatisticalReportByUserID(string month, string userId);
    
    // Các khoản trừ
    Task<Deduct> GetsDeductByMonth(string month);
} 
