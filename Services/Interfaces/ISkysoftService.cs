using ntgroup.Data.Models.Skysofts;

namespace ntgroup.Services;

public interface ISkysoftService
{
    Task<List<Vehicle>> GetsVehicles();
}