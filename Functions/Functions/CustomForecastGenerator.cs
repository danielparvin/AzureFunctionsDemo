using Data;

using Functions.Services;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions.Functions {
    public class CustomForecastGenerator {
        private readonly IForecastService forecastService;
        private readonly ILogger<CustomForecastGenerator> logger;

        public CustomForecastGenerator(IForecastService ForecastService, ILogger<CustomForecastGenerator> Logger) {
            forecastService = ForecastService;
            logger = Logger;
        }

        [Function(nameof(CustomForecastGenerator))]
        public async Task Run([QueueTrigger("custom-forecast-requests")] ForecastCalculationParameters Parameters) {
            await forecastService.GenerateCustomForecast(Parameters);
            logger.LogInformation($"C# Queue trigger function processed.");
        }
    }
}
