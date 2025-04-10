using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForecastCalculationParameters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactorA = table.Column<double>(type: "REAL", nullable: false),
                    FactorB = table.Column<double>(type: "REAL", nullable: false),
                    FactorC = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastCalculationParameters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentMetrics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentName = table.Column<string>(type: "TEXT", nullable: false),
                    MetricName = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentMetrics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FiveDayForecasts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ForecastCalculationParametersID = table.Column<int>(type: "INTEGER", nullable: false),
                    IsStandard = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiveDayForecasts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FiveDayForecasts_ForecastCalculationParameters_ForecastCalculationParametersID",
                        column: x => x.ForecastCalculationParametersID,
                        principalTable: "ForecastCalculationParameters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    FiveDayForecastID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeatherForecast_FiveDayForecasts_FiveDayForecastID",
                        column: x => x.FiveDayForecastID,
                        principalTable: "FiveDayForecasts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiveDayForecasts_ForecastCalculationParametersID",
                table: "FiveDayForecasts",
                column: "ForecastCalculationParametersID");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecast_FiveDayForecastID",
                table: "WeatherForecast",
                column: "FiveDayForecastID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentMetrics");

            migrationBuilder.DropTable(
                name: "WeatherForecast");

            migrationBuilder.DropTable(
                name: "FiveDayForecasts");

            migrationBuilder.DropTable(
                name: "ForecastCalculationParameters");
        }
    }
}
