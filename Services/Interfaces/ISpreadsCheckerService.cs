using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISpreadsCheckerService
{
    /// <summary>
    /// Phiếu checker
    /// GetContractsByNumberCar(string numberCar, string area_SpreadId): báo cáo hợp đồng
    /// GetTimepiecesByNumberCar(string numberCar, string area_SpreadId): báo cáo đồng hồ
    /// GetShiftworksByNumberCar(string numberCar, string area_SpreadId, Banking banking): báo cáo lên ca chưa sử dụng Banking trong Config
    /// GetShiftworksByNumberCar(string numberCar, string area_SpreadId): Báo cáo lên ca không sử dụng lấy thông tin ngân hàng
    /// TotalWalletGSMByNumberCar(string numberCar, string area_SpreadId): lấy tổng tiền ví gsm của 1 phiếu checker 
    /// </summary>
    Task<List<ReportContract>> GetContractsByNumberCar(string numberCar, string area_SpreadId);
    Task<List<ReportTimepiece>> GetTimepiecesByNumberCar(string numberCar, string area_SpreadId);
    Task<ReportTotal> GetShiftworksByNumberCar(string numberCar, string area_SpreadId, Banking banking);
    Task<ReportTotal> GetShiftworksByNumberCar(string numberCar, string area_SpreadId);
    Task<string> TotalWalletGSMByNumberCar(string numberCar, string area_SpreadId);
}
