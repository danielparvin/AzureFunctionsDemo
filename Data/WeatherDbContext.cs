using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data {
    public class WeatherDbContext : DbContext {
        private readonly IConfiguration configuration;

        public WeatherDbContext(IConfiguration Configuration) {
            configuration = Configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WeatherApp"));
        }

        public DbSet<FiveDayForecast> FiveDayForecasts { get; set; }
        public DbSet<InstrumentMetric> InstrumentMetrics { get; set; }
    }
}
