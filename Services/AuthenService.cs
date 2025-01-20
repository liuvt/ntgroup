using System.Net;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Services;
public class AuthenService : IAuthenService
{
    private readonly HttpClient httpClient;
    public AuthenService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }

    public async Task<Driver> Login(DriverDTO login)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<DriverDTO>("api/Auth/Login", login);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(Driver)!;
                }

                return await response.Content.ReadFromJsonAsync<Driver>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                //throw new Exception($"Http status: {response.StatusCode}. Message: {message}");
                throw new Exception(message);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string> Register(DriverDTO register)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<DriverDTO>("api/Auth/Register", register);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return string.Empty;
                }

                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                //throw new Exception($"Http status: {response.StatusCode}. Message: {message}");
                throw new Exception(message);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}