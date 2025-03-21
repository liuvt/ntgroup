using DocumentFormat.OpenXml.Drawing;
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

    public async Task<StatisticalReport> GetsStatisticalReportByMonth(string month)
    {
        try
        {
            var _month = month.Replace("/", "%2F");
            var response = await httpClient.GetAsync($"api/SpreadsReport/Reports/Month/{_month}");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(StatisticalReport)!;
                }

                return await response.Content.ReadFromJsonAsync<StatisticalReport>();
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

    public async Task<StatisticalReport> GetsStatisticalReportByUserID(string month, string userId)
    {
        try
        {
            var _month = month.Replace("/", "%2F");
            var response = await httpClient.GetAsync($"api/SpreadsReport/Reports/Month/{_month}/User/{userId}");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(StatisticalReport)!;
                }

                return await response.Content.ReadFromJsonAsync<StatisticalReport>();
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

    public async Task<Deduct> GetsDeductByMonth(string month)
    {
        try
        {
            var _month = month.Replace("/", "%2F");
            var response = await httpClient.GetAsync($"api/Deduct/Reports/Month/{month}");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(Deduct)!;
                }

                return await response.Content.ReadFromJsonAsync<Deduct>();
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