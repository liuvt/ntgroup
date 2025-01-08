using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsConfigServer
{
    Task<List<Banking>> GetsBankAll ();
    Task<Banking> GetBankById (string bank_Id);
    Task<bool> CreateBank(BankingCreateDTO model);

    Task<List<Area>> GetsAreaAll ();
    Task<Area> GetAreaById (string area_Id);
    Task<bool> CreateArea(AreaCreateDTO model);
    Task<bool> UpdateArea(AreaCreateDTO model);
    Task<bool> DeleteArea(string area_Id);
} 
