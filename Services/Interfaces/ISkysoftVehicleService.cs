using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Services;

public interface ISkysoftVehicleService
{
    Task<List<Vehicle>> GetsVehicles();
}