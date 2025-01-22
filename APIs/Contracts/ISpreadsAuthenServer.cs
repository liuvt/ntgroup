using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsAuthenServer
{
    Task<bool> Register(DriverDTO model);
    Task<string> Login(DriverDTO model);
    Task<List<Driver>> Gets();
} 
