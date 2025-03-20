
using ntgroup.APIs.Contracts;
using ntgroup.Data.Models.Skysofts;
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
            var vehicleData = JsonConvert.DeserializeObject<VehicleTotal>(responseBody);
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
            var result = JsonConvert.DeserializeObject<TripTatol>(responseBody);
            List<Trip> data = result.trips;


            Console.WriteLine("Tổng cuốc: "+ result.count);
            Console.WriteLine("Tiền: "+ result.chargeTotal);
            Console.WriteLine("Thành tiền (nhập thực thu): "+ result.realChargeTotal);
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}