
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace ntgroup.Gateways;

public class HubClients
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public HubClients(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public HttpClient CreateClient(string clientName)
    {
        var client = _httpClientFactory.CreateClient(clientName);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        if (clientName == "Skysoft")
        {
            client.BaseAddress = new Uri(_configuration["Skysoft:URL"] ??
                                throw new InvalidOperationException("Can't found [Secret Key] in appsettings.json !"));
                                
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var input = $"{_configuration["Skysoft:Username"]}-{time}-{_configuration["Skysoft:Key"]}";
            var md5Hash = GenerateMD5(input);
            
            client.DefaultRequestHeaders.Add("time", time.ToString());
            client.DefaultRequestHeaders.Add("md5", md5Hash);
            client.DefaultRequestHeaders.Add("token", _configuration["Skysoft:Token"]);
        }
        else if (clientName == "Localhost")
        {
            client.BaseAddress = new Uri(_configuration["API:localhost"] ?? throw new InvalidOperationException("Localhost URL not found in appsettings.json"));
        }
        
        return client;
    }

    // Truyền 1 object body
    public async Task<HttpResponseMessage> PostAsync(string clientName, string requestUri, object content)
    {
        var client = CreateClient(clientName);
        var jsonBody = JsonConvert.SerializeObject(content);
        var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        return await client.PostAsync(requestUri, httpContent);
    }

    //Generate MD5
    private string GenerateMD5(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
