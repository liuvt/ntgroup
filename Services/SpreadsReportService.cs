using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ntgroup.Data.Models;
using ntgroup.Extensions;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class SpreadsReportService : ISpreadsReportService
{
    private readonly HttpClient httpClient;

    public SpreadsReportService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<StatisticalReportTotal> GetsTotal()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/SpreadsReport/Reports/Totals");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(StatisticalReportTotal)!;
                }

                return await response.Content.ReadFromJsonAsync<StatisticalReportTotal>();
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