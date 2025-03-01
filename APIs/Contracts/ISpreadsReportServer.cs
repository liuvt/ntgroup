using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;
namespace ntgroup.APIs.Contracts;

public interface ISpreadsReportServer
{
    // Lấy toàn bộ thông tin doanh thu lái xe mỗi ngày trong tháng
    Task<List<StatisticalReport>> Gets();
    Task<StatisticalReportTotal> GetsTotal();
    Task<StatisticalReportTotal> GetsTotalbyMonth(string month);
    Task<StatisticalReport> Get(string id);

    // Lấy danh sách thông qua userId
    Task<List<StatisticalReport>> GetsByUserId(string userId);

    // Get thông tin doanh thu lái xe ngày hôm trước (today() - 1)
    Task<List<StatisticalReport>> GetsYesterday();
    // Get tổng doanh thu ngày hôm trước và tính tổng doanh thu
    Task<StatisticalReportTotal> GetsYesterdayTotal();
    // Get thông tin doanh thu lái xe theo ngày
    Task<List<StatisticalReport>> GetsByDay(string byDate);
} 
