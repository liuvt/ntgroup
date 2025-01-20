using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SpreadsConfigService : ISpreadsConfigService
{
    private readonly HttpClient httpClient;

    public SpreadsConfigService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<List<Area>> GetAreas()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/SpreadsConfig/Areas");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(List<Area>)!;
                }

                return await response.Content.ReadFromJsonAsync<List<Area>>();
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

    public async Task<List<Banking>> GetBankings()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/SpreadsConfig/Bankings");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(List<Banking>)!;
                }

                return await response.Content.ReadFromJsonAsync<List<Banking>>();
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