namespace WeatherFrontend.Services
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using WeatherFrontend.Models;

    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService()
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri("https://weather-api-multiendpoint.onrender.com");
        }

        public async Task<WeatherModel> GetWeatherAsync(string city)
        {
            var response = await _http.GetAsync($"weather/{city}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }
        public async Task<string> ReverseTextAsync(string text)
        {
            var body = JsonSerializer.Serialize(new { text });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("reverse", content);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("reversed").GetString();
        }

        public async Task<int?> AddNumbersAsync(int a, int b)
        {
            var response = await _http.GetAsync($"/add?a={a}&b={b}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("result").GetInt32();
            }
            return null;
        }

        public async Task<string> GetCurrentTimeAsync()
        {
            var response = await _http.GetAsync("/time");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("time").GetString();
            }
            return null;
        }

        public async Task<int?> GetRandomNumberAsync()
        {
            var response = await _http.GetAsync("/random");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("random").GetInt32();
            }
            return null;
        }
    }
}