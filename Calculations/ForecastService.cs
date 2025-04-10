using Data;

using Microsoft.EntityFrameworkCore;

namespace Calculations {
    public class ForecastService : IForecastService {
        private readonly WeatherDbContext dbContext;

        public ForecastService(WeatherDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<FiveDayForecast> GenerateCustomForecast(ForecastCalculationParameters Parameters) {
            ICollection<WeatherForecast> forecasts = await generateRandomWeatherForecasts(5);
            var fiveDayForecast = new FiveDayForecast {
                CalculatedOn = DateTime.UtcNow,
                IsStandard = false,
                ForecastCalculationParameters = Parameters,
                WeatherForecasts = forecasts
            };
            dbContext.FiveDayForecasts.Add(fiveDayForecast);
            await dbContext.SaveChangesAsync();
            return fiveDayForecast;
        }

        public async Task<FiveDayForecast> GenerateStandardForecast() {
            ICollection<WeatherForecast> forecasts = await generateRandomWeatherForecasts(5);
            var fiveDayForecast = new FiveDayForecast {
                CalculatedOn = DateTime.UtcNow,
                IsStandard = true,
                ForecastCalculationParameters = new ForecastCalculationParameters {
                    FactorA = 1.0,
                    FactorB = 2.0,
                    FactorC = 3.0
                },
                WeatherForecasts = forecasts
            };
            dbContext.FiveDayForecasts.Add(fiveDayForecast);
            await dbContext.SaveChangesAsync();
            return fiveDayForecast;
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

        public async Task<InstrumentMetric> SaveInstrumentMetric(InstrumentMetric Metric) {
            dbContext.InstrumentMetrics.Add(Metric);
            await dbContext.SaveChangesAsync();
            return Metric;
        }

        /// <summary>
        /// Generates a collection of random weather forecasts (without saving them to the database).
        /// </summary>
        /// <param name="NumberOfDays">The number of forecasts to generate</param>
        /// <returns>New (unsaved) forecasts</returns>
        private static async Task<ICollection<WeatherForecast>> generateRandomWeatherForecasts(int NumberOfDays) {
            var summaries = Enum.GetNames(typeof(ForecastSummary));
            ICollection<WeatherForecast> forecasts = [.. Enumerable.Range(1, NumberOfDays)
                .Select(index => new WeatherForecast {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
            })];
            return forecasts;
        }
    }
}
