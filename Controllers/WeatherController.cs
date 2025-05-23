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
        [HttpGet]

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

        [HttpPost]
        public async Task<IActionResult> Add(int a, int b)
        {
            var result = await _apiService.AddNumbersAsync(a, b);
            ViewBag.SumResult = result;
            return View("AddResult");
        }

        [HttpGet]
        public async Task<IActionResult> Time()
        {
            var currentTime = await _apiService.GetCurrentTimeAsync();
            ViewBag.CurrentTime = currentTime;
            return View("TimeResult");
        }

        [HttpGet]
        public async Task<IActionResult> Random()
        {
            var random = await _apiService.GetRandomNumberAsync();
            ViewBag.RandomNumber = random;
            return View("RandomResult");
        }

    }

}