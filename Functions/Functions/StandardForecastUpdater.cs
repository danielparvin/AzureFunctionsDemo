using Functions.Services;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions.Functions {
    public class StandardForecastUpdater {
        private readonly IForecastService forecastService;
        private readonly ILogger logger;

        public StandardForecastUpdater(IForecastService ForecastService, ILoggerFactory loggerFactory) {
            logger = loggerFactory.CreateLogger<StandardForecastUpdater>();
            forecastService = ForecastService;
        }

        [Function(nameof(StandardForecastUpdater))]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer) { // Every five minutes
            await forecastService.GenerateStandardForecast();
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            if (myTimer.ScheduleStatus is not null) {
                logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
