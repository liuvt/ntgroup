using ntgroup.Data.Models.Skysofts;
using ntgroup.Data.Entities.Skysofts;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISkysoftServer
{
    Task<List<Vehicle>> GetsVehicles();
    Task<List<Trip>> GetsTrips(TripRequestDTO datereport);
    Task<List<Trip>> GetsTripsDate(TripRequestDTO datereport);
} 
