using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.APIs.Contracts;

public interface IShiftworkServer
{
    Task<Shiftwork> Create(ShiftworkCreateDTO models);
    Task<string> CreateShiftworks(List<ShiftworkCreateDTO> models);
    Task<bool> Deleted(string sw_Id);
    Task<bool> Update(ShiftworkDTO models);

}