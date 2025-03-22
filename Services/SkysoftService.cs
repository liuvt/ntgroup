using ntgroup.Data.Models.Skysofts;

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