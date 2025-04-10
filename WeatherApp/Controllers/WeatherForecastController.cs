using Calculations;

using Data;

using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
    private readonly IForecastService forecastService;

    public WeatherForecastController(IForecastService ForecastService) {
        forecastService = ForecastService;
    }

    [HttpPost(nameof(QueueGenerateCustomForecast))]
    public async Task<ActionResult> QueueGenerateCustomForecast(ForecastCalculationParameters Parameters) {
        await forecastService.QueueGenerateCustomForecast(Parameters);
        return Ok();
    }

    [HttpGet(nameof(GetNewestCustomForecast))]
    public async Task<ActionResult<FiveDayForecast>> GetNewestCustomForecast() {
        var forecast = await forecastService.GetNewestCustomForecast();
        if (forecast == null) {
            return NotFound();
        } else {
            return forecast;
        }
    }

    [HttpGet(nameof(GetNewestStandardForecast))]
    public async Task<ActionResult<FiveDayForecast>> GetNewestStandardForecast() {
        var forecast = await forecastService.GetNewestStandardForecast();
        if (forecast == null) {
            return NotFound();
        } else {
            return forecast;
        }
    }
}
