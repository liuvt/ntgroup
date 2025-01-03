using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface IContractService
{
    //Read File
    Task<List<Contract>> GetsByExcel(IBrowserFile _file);
} 
