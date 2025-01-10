using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISpreadsMainService
{
    Task<List<BillContract>> GetContractsByNumberCar(string numberCar, string area_SpreadId);
    Task<List<BillTimepiece>> GetTimepiecesByNumberCar(string numberCar, string area_SpreadId);
    Task<BillShiftwork> GetShiftworksByNumberCar(string numberCar, string area_SpreadId, Banking banking);
    Task<string> TotalWalletGSMByNumberCar(string numberCar, string area_SpreadId);
} 
