using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsAuthenServer
{
    Task<List<Banking>> GetsBankAll ();
    Task<Banking> GetBankById (string bank_Id);
    Task<bool> CreateBank(BankingCreateDTO model);
    Task<bool> UpdateBank(BankingCreateDTO model);
    Task<bool> DeleteBank(string bank_Id);
    Task<bool> DeleteRowBank(string bank_Id);
} 
