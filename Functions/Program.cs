using Calculations;

using Data;

using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();
builder.Services.AddDbContext<WeatherDbContext>();
builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Build().Run();
