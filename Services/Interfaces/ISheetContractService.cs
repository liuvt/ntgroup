using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISheetContractService
{
    Task<List<BillContract>> Gets(string numberCar);
} 
