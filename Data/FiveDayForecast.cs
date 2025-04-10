namespace Data {
    public class FiveDayForecast {
        public int ID { get; set; }
        public DateTime CalculatedOn { get; set; }
        public ForecastCalculationParameters ForecastCalculationParameters { get; set; }
        public bool IsStandard { get; set; }
        public ICollection<WeatherForecast> WeatherForecasts { get; set; }
    }
}
