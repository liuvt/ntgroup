using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Services;

public interface IAuthenService
{
    Task<string> Login(DriverLoginDTO login);
    Task<bool> Register(DriverRegisterDTO register);
    
    // Authen
    Task LogOut();
    Task<bool> CheckAuthenState();
    Task<AuthenticationState> GetAuthenState();

    // User
    Task<UserRole> GetUserAuth();
}