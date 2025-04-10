using Data;

namespace WeatherApp.Services {
    public interface IForecastService {
        Task<FiveDayForecast?> GetNewestCustomForecast();
        Task<FiveDayForecast?> GetNewestStandardForecast();
        Task QueueGenerateCustomForecast(ForecastCalculationParameters Parameters);
    }
}
