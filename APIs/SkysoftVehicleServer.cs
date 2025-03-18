using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using System.Globalization;
using ntgroup.Extensions;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ntgroup.Gateways;

namespace ntgroup.APIs;

public class SkysoftVehicleServer : ISkysoftVehicleServer
{
    protected readonly IConfiguration configuration;
    private readonly HubClients hubClients;
    public SkysoftVehicleServer(IConfiguration _configuration, HubClients _hubClients)
    {
        this.configuration = _configuration;
        hubClients = _hubClients;
    }

    public async Task<List<Vehicle>> PostHTTPToSkysoftVehicles()
    {
        try
        {
            object objectContent = new
            {
            };

            //truyền ObjectContent vào
            var response = await hubClients.PostAsync("Skysoft","api/online_taxi", objectContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            var vehicleData = JsonConvert.DeserializeObject<SkysoftVehicle>(responseBody);
            List<Vehicle> vehicles = vehicleData.vehicles;

            return vehicles;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Trip>> PostHTTPToSkysoftTrips(string datereport)
    {
        try
        {
            // Thêm tham số 'date' vào body
            object objectContent = new
            {
                date = datereport // Định dạng ngày phù hợp với yêu cầu API
            };

            var response = await hubClients.PostAsync("Skysoft","api/query_taxi_trips", objectContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SkysoftTrip>(responseBody);
            List<Trip> _trips = result.trips;

            return _trips;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}