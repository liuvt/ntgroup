using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Services;

public interface IAliService
{
    Task<string> GetStringAli();
}