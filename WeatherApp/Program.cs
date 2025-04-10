using Data;

using WeatherApp.Services;

namespace WeatherApp {
    public static class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<WeatherDbContext>();
            builder.Services.AddScoped<IForecastService, ForecastService>();
            builder.Services.AddCors(options =>
                options.AddPolicy(
                    "AllowReactApp",
                    builder => builder.WithOrigins("http://localhost:63464").AllowAnyHeader().AllowAnyMethod()
                )
            );
            var app = builder.Build();
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowReactApp");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapFallbackToFile("index.html");
            app.MapControllers();
            app.Run();
        }
    }
}
