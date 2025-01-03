using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISheetShiftworkService
{
    Task<BillShiftwork> Gets(string numberCar);
} 
