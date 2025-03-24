using ntgroup.Data.Entities.Skysofts;
using ntgroup.Data.Models.Skysofts;

namespace ntgroup.Services;

public interface ISkysoftService
{
    Task<List<Vehicle>> GetsVehicles();
    Task<List<TripDTO>> GetsTrips(TripRequestDTO datereport);
}