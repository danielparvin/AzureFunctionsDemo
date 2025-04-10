using Data;

namespace Functions.Services {
    public interface IForecastService {
        Task<FiveDayForecast> GenerateCustomForecast(ForecastCalculationParameters Parameters);
        Task<FiveDayForecast> GenerateStandardForecast();
        Task<InstrumentMetric> SaveInstrumentMetric(InstrumentMetric Metric);
    }
}
