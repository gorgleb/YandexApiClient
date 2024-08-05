using System.Net;
using System.Text.Json;
using GeoSuggest.GeosuggestClient.Contracts;
using GeoSuggest.GeosuggestClient.Settings;
using GeoSuggest.SharedKernel.Exceptions;
using Microsoft.Extensions.Options;

namespace GeoSuggest.GeosuggestClient;

public class GeosuggestClient
{
    private readonly HttpClient _httpClient;
    private readonly GeosuggestClientOptions _settings;
    private readonly JsonSerializerOptions _jsonOptions;

    public GeosuggestClient(HttpClient httpClient, IOptionsMonitor<GeosuggestClientOptions> settings)
    {
        _httpClient = httpClient;
        _settings = settings.CurrentValue;
        _jsonOptions = new JsonSerializerOptions { WriteIndented = false };
    }

    public async Task<SuggestResponse> SearchAsync(string search, int take)
    {
        var url = BuildSearchUrl(search, take);
        return await GetAsync<SuggestResponse>(url);
    }

    private string BuildSearchUrl(string search, int take)
    {
        return $"{_settings.BaseUrl}{string.Format(_settings.SuggestPath, search)}&results={take}&apikey={_settings.ApiKey}";
    }

    private async Task<TResult> GetAsync<TResult>(string url)
    {
        using var response = await _httpClient.GetAsync(url);
        var contentString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return DeserializeSuccessfulResponse<TResult>(contentString);
        }

        HandleUnsuccessfulResponse(response.StatusCode, contentString);
        return default;
    }

    private TResult DeserializeSuccessfulResponse<TResult>(string contentString)
    {
        if (string.IsNullOrWhiteSpace(contentString))
        {
            return default;
        }

        return JsonSerializer.Deserialize<TResult>(contentString, _jsonOptions);
    }

    private void HandleUnsuccessfulResponse(HttpStatusCode statusCode, string contentString)
    {
        switch (statusCode)
        {
            case HttpStatusCode.TooManyRequests:
                throw new TooManyRequestsException("Превышен лимит запросов к апи геосаджеста");
            case HttpStatusCode.NotFound:
                // Сервис при попытке поиска по несуразному тексту вида "ываыва" возвращает статус 200
                return;

            default:
                throw new HttpRequestException(contentString, null, statusCode: statusCode);
        }
    }
}