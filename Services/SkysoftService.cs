using ntgroup.Data.Models.Skysofts;
using ntgroup.Data.Entities.Skysofts;

namespace ntgroup.Services;
public class SkysoftService : ISkysoftService
{
    private readonly HttpClient httpClient;
    public SkysoftService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<List<Vehicle>> GetsVehicles()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Skysoft/Vehicles");

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

    public async Task<List<TripDTO>> GetsTrips(TripRequestDTO datereport)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"api/Skysoft/Trips", datereport);

            Console.WriteLine("datereport: "+ datereport.DateReport.ToString());
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new List<TripDTO>();
                }

                var result = await response.Content.ReadFromJsonAsync<List<TripDTO>>();
                return result ?? new List<TripDTO>(); // Đảm bảo không trả về null
            }
            else
            {
                var mess = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi API ({response.StatusCode}): {mess}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            throw new Exception($"Lỗi kết nối API: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi hệ thống: {ex.Message}");
        }
    }

}