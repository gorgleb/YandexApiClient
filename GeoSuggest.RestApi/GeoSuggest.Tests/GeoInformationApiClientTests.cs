using GeoSuggest.ApiClient;
using GeoSuggest.ApiClient.Settings;
using GeoSuggest.RestApi.V1.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace GeoSuggest.Tests
{
    public class GeoInformationApiClientTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly GeoInformationApiClientOptions _options;
        private readonly GeoInformationApiClient _client;

        public GeoInformationApiClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _options = new GeoInformationApiClientOptions
            {
                BaseUrl = "http://you-dont-need-to-know-dat",
                ApiEndpoint = "you-dont-need-to-know-dat",
                TimeoutSeconds = 30
            };
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_options.BaseUrl)
            };

            _client = new GeoInformationApiClient(httpClient, Options.Create(_options));
        }

        [Fact]
        public async Task SuggestRelevantAsync_SuccessfulResponse_ReturnsAddressDisplay()
        {
            var addressString = "невский 26";
            var expectedAddress = new AddressDisplay
            {
                Country = new CountryDisplay { Name = "Россия" },
                Region = null,
                City = new CityDisplay { Name = "Санкт-Петербург" },
                Street = "проспект Невский",
                House = "26",
                FormattedAddress = "Санкт-Петербург, проспект Невский, 26"
            };

            // Act
            var result = await _client.SuggestRelevantAsync(addressString);

            // Assert
            Assert.Equal(expectedAddress.FormattedAddress, result.FormattedAddress);
        }

        [Fact]
        public async Task SuggestRelevantAsync_BadRequest_ThrowsArgumentException()
        {
            // Arrange
            var addressString = "hehehehehehe";
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonSerializer.Serialize(new ProblemDetails { Detail = "Bad Request" }))
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _client.SuggestRelevantAsync(addressString));
        }
    }
}