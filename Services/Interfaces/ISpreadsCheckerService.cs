using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISpreadsCheckerService
{
    Task<List<ReportContract>> GetContractsByNumberCar(string numberCar, string area_SpreadId);
    Task<List<ReportTimepiece>> GetTimepiecesByNumberCar(string numberCar, string area_SpreadId);
    Task<ReportTotal> GetShiftworksByNumberCar(string numberCar, string area_SpreadId, Banking banking); // Thông tin có chưa banking chuẩn SpreadsConfig
    Task<string> TotalWalletGSMByNumberCar(string numberCar, string area_SpreadId);

    Task<ReportTotal> GetShiftworksByNumberCar(string numberCar, string area_SpreadId); // Thông tin mặc định
    Task<List<ReportTotal>> GetShiftworks(string area_SpreadId);
} 
