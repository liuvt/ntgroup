
using ntgroup.APIs.Contracts;
using ntgroup.Data.Models.Skysofts;
using Newtonsoft.Json;
using ntgroup.Gateways;
using System.Globalization;
using ntgroup.Data.Entities.Skysofts;

namespace ntgroup.APIs;

public class SkysoftServer : ISkysoftServer
{
    #region Constructor and services
    protected readonly IConfiguration configuration;
    private readonly HubClients hubClients;
    public SkysoftServer(IConfiguration _configuration, HubClients _hubClients)
    {
        this.configuration = _configuration;
        hubClients = _hubClients;
    }
    #endregion

    #region Vehicles
    public async Task<List<Vehicle>> GetsVehicles()
    {
        try
        {
            object objectContent = new
            {
            };

            //truyền ObjectContent vào
            var response = await hubClients.PostAsync("Skysoft", "api/online_taxi", objectContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            var vehicleData = JsonConvert.DeserializeObject<VehicleTotal>(responseBody);
            List<Vehicle> vehicles = vehicleData.vehicles;

            return vehicles;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    #endregion

    #region Trips
    //Get all trip in date
    public async Task<List<Trip>> GetsTrips(TripRequestDTO datereport)
    {
        try
        {
            // Thêm tham số 'date' vào body
            object objectContent = new
            {
                date = datereport.DateReport // Định dạng ngày phù hợp với yêu cầu API
            };

            var response = await hubClients.PostAsync("Skysoft", "api/query_taxi_trips", objectContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TripTatol>(responseBody);
            List<Trip> _trips = result.trips;
            return _trips;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Get all trip in date
    public async Task<List<Trip>> GetsTripsDate(TripRequestDTO datereport)
    {
        try
        {
            //Lùi ngày xuất dữ liệu

            var _datereport = DateTime.ParseExact(datereport.DateReport, "yyyyMMdd", CultureInfo.InvariantCulture).AddDays(-1);
            // Thêm tham số 'date' vào body
            object objectContent = new
            {
                date = _datereport.ToString("yyyyMMdd") // Định dạng YYYYMMDD phù hợp với yêu cầu API
            };

            var response = await hubClients.PostAsync("Skysoft", "api/query_taxi_trips", objectContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TripTatol>(responseBody);
            List<Trip> _trips = result.trips;
            return _trips;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}