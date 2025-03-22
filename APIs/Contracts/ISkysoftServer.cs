using ntgroup.Data.Models.Skysofts;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISkysoftServer
{
    Task<List<Vehicle>> GetsVehicles();
    Task<List<Trip>> GetsTrips(string datereport);
    Task<List<Trip>> GetsTripsDate(string datereport);
} 
