using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net;
using ntgroup.Repositories.Interfaces;
using ntgroup.Data.Entities;

namespace ntgroup.Repositories;

public class TimepieceRepository : ITimepieceRepository
{
    private readonly HttpClient httpClient;

    //Constructor
    public TimepieceRepository(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<bool> Create(List<TimepieceDTO> models)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<List<TimepieceDTO>>($"api/TimePiece/Create", models);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return false;
                }

                return true;
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
