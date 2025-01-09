using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsConfigServer
{
    Task<List<Banking>> GetsBankAll ();
    Task<Banking> GetBankById (string bank_Id);
    Task<bool> CreateBank(BankingCreateDTO model);
    Task<bool> UpdateBank(BankingCreateDTO model);
    Task<bool> DeleteBank(string bank_Id);
    Task<bool> DeleteRowBank(string bank_Id);

    Task<List<Area>> GetsAreaAll ();
    Task<Area> GetAreaById (string area_Id);
    Task<bool> CreateArea(AreaCreateDTO model);
    Task<bool> UpdateArea(AreaCreateDTO model);
    Task<bool> DeleteArea(string area_Id);
    Task<bool> DeleteRowArea(string area_Id);
} 
