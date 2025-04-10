using System.Text.Json;

using Azure.Storage.Queues;

using Data;

using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Services {
    public class ForecastService : IForecastService {
        private readonly WeatherDbContext dbContext;
        private readonly QueueClient queueClient;

        public ForecastService(WeatherDbContext dbContext, IConfiguration Configuration) {
            this.dbContext = dbContext;
            queueClient = new QueueClient(
                Configuration.GetSection("AzureWebJobsStorage").Value,
                Configuration.GetSection("ForecastQueueName").Value,
                new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
        }

        public async Task<FiveDayForecast?> GetNewestCustomForecast() {
            return dbContext.FiveDayForecasts
                .Where(f => !f.IsStandard)
                .OrderByDescending(f => f.CalculatedOn)
                .Include(f => f.ForecastCalculationParameters)
                .Include(f => f.WeatherForecasts)
                .FirstOrDefault();
        }

        public async Task<FiveDayForecast?> GetNewestStandardForecast() {
            return dbContext.FiveDayForecasts
                .Where(f => f.IsStandard)
                .OrderByDescending(f => f.CalculatedOn)
                .Include(f => f.ForecastCalculationParameters)
                .Include(f => f.WeatherForecasts)
                .FirstOrDefault();
        }

        public async Task QueueGenerateCustomForecast(ForecastCalculationParameters Parameters) {
            string serializedParameters = JsonSerializer.Serialize(Parameters);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(serializedParameters);
        }
    }
}
