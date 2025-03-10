using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SpreadsRecuitmentService : ISpreadsRecuitmentService
{
    private readonly HttpClient httpClient;

    public SpreadsRecuitmentService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<List<Job>> GetsJobs()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Jobs");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(List<Job>)!;
                }

                return await response.Content.ReadFromJsonAsync<List<Job>>();
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