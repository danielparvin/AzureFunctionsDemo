using System.Text.Json;

using Data;

using Functions.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions.Functions {
    public class InstrumentMetricsListener {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new() {
            PropertyNameCaseInsensitive = true
        };
        private readonly IForecastService forecastService;
        private readonly ILogger<InstrumentMetricsListener> logger;

        public InstrumentMetricsListener(IForecastService ForecastService, ILogger<InstrumentMetricsListener> Logger) {
            forecastService = ForecastService;
            logger = Logger;
        }

        [Function(nameof(InstrumentMetricsListener))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest Request) {
            InstrumentMetric? metric;
            try {
                metric = await JsonSerializer.DeserializeAsync<InstrumentMetric>(Request.Body, jsonSerializerOptions);
            } catch (Exception ex) {
                logger.LogError(ex, "Failed to deserialize request body");
                return new BadRequestObjectResult("Invalid request body");
            }
            if (metric == null) {
                logger.LogError("Deserialized metric is null");
                return new BadRequestObjectResult("Invalid request body");
            }

            InstrumentMetric savedMetric = await forecastService.SaveInstrumentMetric(metric);
            logger.LogInformation($"Saved instrument metric: {savedMetric.ID}");
            return new OkObjectResult(savedMetric);
        }
    }
}
