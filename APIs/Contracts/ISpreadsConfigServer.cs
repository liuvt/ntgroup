using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsConfigServer
{
    Task<List<Banking>> GetsBankAll ();

    Task<Banking> GetBankById (string bank_Id);
} 
