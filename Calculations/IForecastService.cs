using Data;

namespace Calculations {
    public interface IForecastService {
        Task<FiveDayForecast> GenerateCustomForecast(ForecastCalculationParameters Parameters);
        Task<FiveDayForecast> GenerateStandardForecast();
        Task<FiveDayForecast?> GetNewestCustomForecast();
        Task<FiveDayForecast?> GetNewestStandardForecast();
        Task<InstrumentMetric> SaveInstrumentMetric(InstrumentMetric Metric);
    }
}
