using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISheetShiftworkService
{
    Task<BillShiftwork> Get(string numberCar);
    Task<BillShiftwork> GetKG(string numberCar);
} 
