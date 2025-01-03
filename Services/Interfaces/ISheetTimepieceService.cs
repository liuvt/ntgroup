using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISheetTimepieceService
{
    Task<List<BillTimepiece>> Gets(string numberCar);
} 
