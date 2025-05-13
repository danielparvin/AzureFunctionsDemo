# Azure Functions Demo
## Description
This solution demonstrates how to refactor API endpoints into Azure functions using timer, queue, and HTTP triggers.

## Branches
- initial -- This branch contains the initial code for the GenerateStandardForecast, GenerateCustomForecast, and SubmitInstrumentMetric HTTP endpoints.
- refactored -- This branch demonstrates the result of refactoring the three endpoints into TimerTrigger, QueueTrigger, and HttpTrigger Azure functions.
- main -- This branch provides a simple UI to visualize the back-end refactor.

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
7. Replace `[USERNAME]` with your username in the WeatherApp's `appsettings.json` file.
8. In the terminal, change directory to the `WeatherApp` folder and execute the following command to create the database:
   ```bash
   dotnet ef database update
   ```
 9. In Visual Studio, set "Web App and Functions" as the startup project and click "Start."

## Refactoring
To be continued...