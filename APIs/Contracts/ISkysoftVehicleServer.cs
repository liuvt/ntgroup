using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISkysoftVehicleServer
{
    Task<List<Vehicle>> PostHTTPToSkysoftVehicles();
    Task<List<Trip>> PostHTTPToSkysoftTrips(string datereport);
} 
