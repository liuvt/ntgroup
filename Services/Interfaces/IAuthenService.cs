using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Services;

public interface IAuthenService
{
    Task<Driver> Login(DriverDTO login);
    Task<string> Register(DriverDTO register);
}