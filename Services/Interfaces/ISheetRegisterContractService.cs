using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISheetRegisterContractService
{
    Task<List<DefaultContract>> Gets();
} 
