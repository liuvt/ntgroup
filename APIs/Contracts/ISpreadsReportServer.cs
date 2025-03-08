using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;
namespace ntgroup.APIs.Contracts;

public interface ISpreadsReportServer
{
    // Chi tiết doanh thu của lái xe
    Task<List<StatisticalReportDetail>> GetsStatisticalReportDetail();
    // Đếm tổng số bản gi doanh thu theo tháng
    Task<StatisticalReport> GetsStatisticalReportByMonth(string month);
    Task<StatisticalReport> GetsStatisticalReportByUserID(string month, string userId);
} 
