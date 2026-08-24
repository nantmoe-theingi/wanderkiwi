using System.Net.Http.Json;
using System.Text.Json;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.Infrastructure.Services;

public sealed class OpenRouteServiceOptions
{
    public const string SectionName = "OpenRouteService";
    public string ApiKey { get; set; } = string.Empty;
}

public class OpenRouteService : IRouteService
{
    private readonly HttpClient _httpClient;
    private readonly OpenRouteServiceOptions _options;

    public OpenRouteService(HttpClient httpClient, Microsoft.Extensions.Options.IOptions<OpenRouteServiceOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<int> GetDrivingMinutesAsync(double originLatitude, double originLongitude,
        double destinationLatitude, double destinationLongitude)
    {
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            throw new InvalidOperationException("OpenRouteService is not configured.");
        }

        using var request = new HttpRequestMessage(HttpMethod.Post, "v2/directions/driving-car")

        {
            Content = JsonContent.Create(new
            {
                coordinates = new[]
                {
                    new[] { originLongitude, originLatitude },
                    new[] { destinationLongitude, destinationLatitude }
                }
            })
        };
        request.Headers.TryAddWithoutValidation("Authorization", _options.ApiKey);

        using var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"OpenRouteService returned {(int)response.StatusCode}: {error}");
        }
        using var document = JsonDocument.Parse(await response.Content.ReadAsStreamAsync());
        var seconds = document.RootElement.GetProperty("routes")[0]
            .GetProperty("summary").GetProperty("duration").GetDouble();

        return Math.Max(1, (int)Math.Ceiling(seconds / 60));
    }
}
