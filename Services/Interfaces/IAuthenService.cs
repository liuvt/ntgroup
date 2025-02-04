using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Services;

public interface IAuthenService
{
    Task<string> Login(DriverDTO login);
    Task<bool> Register(DriverDTO register);
    
    // Authen
    Task LogOut();
    Task<bool> CheckAuthenState();
    Task<AuthenticationState> GetAuthenState();
}