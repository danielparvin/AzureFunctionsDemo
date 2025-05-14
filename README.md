# Azure Functions Demo
## Description
This solution demonstrates how to refactor API endpoints into Azure functions using timer, queue, and HTTP triggers. Read the "Refactoring" section below for details.

## Branches
- `initial` -- This branch contains the initial code for the GenerateStandardForecast, GenerateCustomForecast, and SubmitInstrumentMetric HTTP endpoints.
- `refactored` -- This branch demonstrates the result of refactoring the three endpoints into TimerTrigger, QueueTrigger, and HttpTrigger Azure functions.
- `main` -- This branch provides a simple UI to visualize the back-end refactor.

## Running the Demo
1. Install the latest [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Install the .NET EF Core CLI by executing the following command in the terminal:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
3. Install the latest version of [Visual Studio 2022](https://visualstudio.microsoft.com/vs/).
4. Install the [Autostart Azurite on run](https://marketplace.visualstudio.com/items?itemName=PerMalmberg.AzuriteAutoStarterAsBackgroundProcess) extension in Visual Studio.
5. Clone the repository.
6. Open the solution in Visual Studio.
7. Replace `[USERNAME]` with your username in the WeatherApp project's `appsettings.json` file and in the Functions project's `local.settings.json` file.
8. In the terminal, change directory to the `WeatherApp` folder and execute the following command to create the database:
   ```bash
   dotnet ef database update
   ```
 9. In Visual Studio, set "Web App and Functions" as the startup project and click "Start."

## Refactoring
The `initial` branch contains five HTTP endpoints in `WeatherApp.Controllers.WeatherForecastController`:
- `GenerateCustomForecast`
- `GenerateStandardForecast`
- `GetNewestCustomForecast`
- `GetNewestStandardForecast`
- `SubmitInstrumentMetric`

The `GetNewestCustomForecast` and `GetNewestStandardForecast` endpoints simply return a `FiveDayForecast` from the database. The other three endpoints (`GenerateCustomForecast`, `GenerateStandardForecast`, and `SubmitInstrumentMetric`) are the subjects of the refactor.

### GenerateCustomForecast
The refactor of the `GenerateCustomForecast` controller endpoint is the most complex of the three updates. The endpoint receives a `ForecastCalculationParameters` object to customize the factors that the forecast calculator uses to generate a forecast. (In this demo, the custom calculation parameters don't actually affect the forecast calculation, which is simply random. But this demo simulates the possibility of dynamically modifying the calculation through user input.) The `refactored` branch replaces the `GenerateCustomForecast` endpoint with a new `QueueGenerateCustomForecast` endpoint. Instead of immediately generating and returning a custom forecast, this new endpoint adds a request message to a queue and returns a `200` status to the user. The new `CustomForecastGenerator` function receives and processes the queue message with a `QueueTrigger` to generate and save the custom forecast. Hypothetically, if the custom-forecast-generation process took a few minutes, this design would allow users to submit the custom-forecast requests quickly. Users would then retrieve the results once the results are ready.

### GenerateStandardForecast
In the `initial` branch, the `GenerateStandardForecast` endpoint listens for an HTTP request to trigger the generation of a new standard forecast. But it makes more sense to generate a standard forecast automatically every few minutes. So the `refactored` branch contains a `StandardForecastUpdater` function that replaces the HTTP endpoint with a `TimerTrigger`. The function uses the same models and services that the original controller method used, making the refactor relatively straightforward.

### SubmitInstrumentMetric
The refactor removes the `SubmitInstrumentMetric` from the API controller and replaces it with an Azure Function. In this case, the function uses an `HttpTrigger` to listen for new `InstrumentMetric` requests. The function simulates an HTTP endpoint that meteorological instruments could `POST` to. An `HttpTrigger` function like this is useful for distinguishing such an endpoint from the main web API.
