using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISpreadsConfigService
{
    Task<List<Area>> GetAreas();
    Task<List<Banking>> GetBankings();
} 
