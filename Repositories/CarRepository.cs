using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net;
using ntgroup.Repositories.Interfaces;
using ntgroup.Data.Entities;

namespace ntgroup.Repositories;

public class CarRepository : ICarRepository
{
    private readonly HttpClient httpClient;

    //Constructor
    public CarRepository(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }
    public async Task<IEnumerable<CarDTO>> Gets()
    {
        try
        {
            var result = await this.httpClient.GetAsync($"api/Car");
            if ( result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CarDTO>();
                }
                return await result.Content.ReadFromJsonAsync<IEnumerable<CarDTO>>()
                    ?? throw new Exception("Cannot deserialize the response");
            }
            else
            {
                var message = await result.Content.ReadAsStringAsync();
                //throw new Exception($"Http status: {response.StatusCode}. Message: {mess}");
                throw new Exception(message);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CarDTO> Get(string car_Id)
    {
        try
        {
            var result = await httpClient.GetAsync($"api/Car/{car_Id}");
            if (result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CarDTO) ?? throw new Exception("NoContent");
                }

                return await result.Content.ReadFromJsonAsync<CarDTO>()
                        ?? throw new Exception("Cannot deserialize the response");
            }
            else
            {
                var message = await result.Content.ReadAsStringAsync();
                //throw new Exception($"Http status: {response.StatusCode}. Message: {mess}");
                throw new Exception(message);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
