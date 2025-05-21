namespace WeatherFrontend.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeatherFrontend.Models;
    using System.Threading.Tasks;
    using WeatherFrontend.Services;

    public class WeatherController : Controller
    {
        private readonly ApiService _apiService;

        public WeatherController()
        {
            _apiService = new ApiService();
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weather = await _apiService.GetWeatherAsync(city);
            return View("WeatherResult", weather);
        }

        [HttpPost]
        public async Task<IActionResult> Reverse(string input)
        {
            var reversed = await _apiService.ReverseTextAsync(input);
            ViewBag.Reversed = reversed;
            return View("ReverseResult");
        }
    }

}