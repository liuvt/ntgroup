using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ntgroup.Data.Entities;
using ntgroup.Extensions;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using DocumentFormat.OpenXml.Office.MetaAttributes;
using ntgroup.Data.Models;

namespace ntgroup.Services;
public class AliService : IAliService
{
    public async Task<string> GetStringAli()
    {
        throw new NotImplementedException();
    }
}