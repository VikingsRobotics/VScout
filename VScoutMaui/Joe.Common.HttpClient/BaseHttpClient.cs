using System.Text;
using System.Text.Json;

namespace Joe.Common.HttpClient;

public abstract class BaseHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public BaseHttpClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAsync(string endpoint)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        string stringResult = Encoding.Default.GetString(await response.Content.ReadAsByteArrayAsync());
        return stringResult;
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        string stringResult = Encoding.Default.GetString(await response.Content.ReadAsByteArrayAsync());

        return JsonSerializer.Deserialize<T>(stringResult) ?? throw new InvalidOperationException("Could not deserialize object");
    }
}