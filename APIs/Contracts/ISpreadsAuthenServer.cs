using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsAuthenServer
{
    Task<string> Register(DriverDTO model);
    Task<string> Login(DriverDTO model);
} 
