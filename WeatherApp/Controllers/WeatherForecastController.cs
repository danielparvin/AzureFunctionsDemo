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

    [HttpPost("GenerateCustomForecast")]
    public async Task<ActionResult> GenerateCustomForecast(ForecastCalculationParameters Parameters) {
        var forecast = await forecastService.GenerateCustomForecast(Parameters);
        return CreatedAtAction(nameof(GetNewestCustomForecast), new { id = forecast.ID }, forecast);
    }

    [HttpPost("GenerateStandardForecast")]
    public async Task<ActionResult> GenerateStandardForecast() {
        var forecast = await forecastService.GenerateStandardForecast();
        return CreatedAtAction(nameof(GetNewestStandardForecast), new { id = forecast.ID }, forecast);
    }

    [HttpGet("GetNewestCustomForecast")]
    public async Task<ActionResult<FiveDayForecast>> GetNewestCustomForecast() {
        var forecast = await forecastService.GetNewestCustomForecast();
        if (forecast == null) {
            return NotFound();
        } else {
            return forecast;
        }
    }

    [HttpGet("GetNewestStandardForecast")]
    public async Task<ActionResult<FiveDayForecast>> GetNewestStandardForecast() {
        var forecast = await forecastService.GetNewestStandardForecast();
        if (forecast == null) {
            return NotFound();
        } else {
            return forecast;
        }
    }

    [HttpPost("SubmitInstrumentMetric")]
    public async Task<ActionResult> SubmitInstrumentMetric(InstrumentMetric Metric) {
        var savedMetric = await forecastService.SaveInstrumentMetric(Metric);
        return CreatedAtAction(nameof(SubmitInstrumentMetric), new { id = savedMetric.ID }, savedMetric);
    }
}
