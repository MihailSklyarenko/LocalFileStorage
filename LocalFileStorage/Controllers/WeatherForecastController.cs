using LocalFileStorageData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalFileStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStorageService _storageService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStorageService storageService)
        {
            _logger = logger;
            _storageService = storageService;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<ActionResult> Get(IFormFile file)
        {
            await _storageService.AddAsync(file);

            return Ok();
        }
    }
}