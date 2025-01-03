using ntgroup.Data.Entities;

namespace ntgroup.APIs.Contracts;

public interface IDriverServer
{
    Task<IEnumerable<DriverDTO>> Gets(); 
    Task<DriverDTO> Get(string driver_Id); 
    Task<DriverDTO> Create(DriverCreateDTO model);
    Task<string> CreateDrivers(List<DriverCreateDTO> models);
    Task<DriverDTO> Update(CarUpdateDTO model); 
    Task<bool> Delete(string driver_Id); 
    Task<string> DeleteDrivers(List<string> listDrivers);
}