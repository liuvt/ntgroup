using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ITimepieceService
{
    //Read File
    Task<List<TimepieceDTO>> GetsByExcel(IBrowserFile _file);
} 
