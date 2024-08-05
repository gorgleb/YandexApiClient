using GeoSuggest.ApiClient.Interfaces;
using GeoSuggest.ApiClient.Settings;
using GeoSuggest.RestApi.V1.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace GeoSuggest.ApiClient
{
    /// <summary>
    /// Предоставляет функционал клиента для обращения к сервису географических данных.
    /// </summary>
    public class GeoInformationApiClient : IGeoInformationApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly GeoInformationApiClientOptions _options;

        public GeoInformationApiClient(HttpClient httpClient, IOptions<GeoInformationApiClientOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        }

        /// <summary>
        /// Возвращает ответ от GeoSuggest.RestApi, содержащий информацию по запрашиваемому адресу.
        /// </summary>
        /// <param name="addressString">Строка адреса, по которой необходимо провести запрос</param>
        public async Task<AddressDisplay> SuggestRelevantAsync(string addressString)
        {
            var response = await SendRequestAsync(addressString);
            await EnsureSuccessResponseAsync(response);
            return await DeserializeResponseAsync<AddressDisplay>(response);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(string addressString)
        {
            var encodedAddress = Uri.EscapeDataString(addressString);
            return await _httpClient.GetAsync($"{_options.ApiEndpoint}?addressString={encodedAddress}");
        }

        private async Task EnsureSuccessResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            var problemDetails = await DeserializeProblemDetailsAsync(response);
            throw CreateExceptionForStatusCode(response.StatusCode, problemDetails);
        }

        private Exception CreateExceptionForStatusCode(HttpStatusCode statusCode, ProblemDetails problemDetails)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => new ArgumentException(problemDetails?.Detail ?? "Предоставлена некорректная строка адреса."),
                HttpStatusCode.TooManyRequests => new InvalidOperationException(problemDetails?.Detail ?? "Слишком много запросов. Пожалуйста, повторите попытку позже."),
                HttpStatusCode.InternalServerError => new HttpRequestException(problemDetails?.Detail ?? "Произошла внутренняя ошибка сервера."),
                _ => new HttpRequestException($"Произошла непредвиденная ошибка. Код состояния: {statusCode}")
            };
        }

        private async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }

        private async Task<ProblemDetails> DeserializeProblemDetailsAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<ProblemDetails>(content);
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}