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
public class SkysoftVehicleService : ISkysoftVehicleService
{
    private readonly HttpClient httpClient;
    public SkysoftVehicleService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<List<Vehicle>> GetsVehicles()
    {
        try
        {
            Console.WriteLine(httpClient.BaseAddress);
            var response = await httpClient.GetAsync($"api/Vehicle");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(List<Vehicle>)!;
                }

                return await response.Content.ReadFromJsonAsync<List<Vehicle>>();
            }
            else
            {
                var mess = await response.Content.ReadAsStringAsync();
                throw new Exception(mess);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

   
}