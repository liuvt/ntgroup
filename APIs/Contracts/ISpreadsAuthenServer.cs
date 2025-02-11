using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsAuthenServer
{
    Task<bool> Register(DriverRegisterDTO model);
    Task<string> Login(DriverLoginDTO model);
    Task<List<Driver>> Gets();
} 
