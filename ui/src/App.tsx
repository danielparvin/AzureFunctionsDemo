import { useEffect, useState } from 'react'
import './App.css'

interface Forecast {
    id: number;
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

interface ForecastCalculationParameters {
    id: number;
    factorA: number;
    factorB: number;
    factorC: number;
}

interface FiveDayForecast {
    id: number;
    calculatedOn: string;
    forecastCalculationParameters: ForecastCalculationParameters[];
    isStandard: boolean;
    weatherForecasts: Forecast[];
}
function App() {
    const [standardForecast, setStandardForecast] = useState<FiveDayForecast | null>(null);
    const [customForecast, setCustomForecast] = useState<FiveDayForecast | null>(null);
    const [customForecastParams, setCustomForecastParams] = useState<ForecastCalculationParameters>({
        id: 0,
        factorA: 0,
        factorB: 0,
        factorC: 0
    });

    useEffect(() => {
        populateWeatherData();
    }, []);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setCustomForecastParams(prevState => ({
            ...prevState,
            [name]: Number(value)
        }));
    };

    const submitCustomForecastParams = async () => {
        const response = await fetch('https://localhost:7272/WeatherForecast/QueueGenerateCustomForecast', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(customForecastParams)
        });

        if (response.ok) {
            alert('Custom forecast parameters submitted successfully!');
        } else {
            alert('Failed to submit custom forecast parameters.');
        }
    };

    const standardForecastTable = standardForecast === null
        ? <p><em>Loading... </em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {standardForecast.weatherForecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    const customForecastTable = customForecast === null
        ? <p><em>Submit a custom forecast request to view the latest custom forecast!</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {customForecast.weatherForecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    const customForecastParamsForm =
        <form onSubmit={(e) => { e.preventDefault(); submitCustomForecastParams(); }}>
            <div>
                <label className="m-2">Factor A: </label>
                <input type="number" name="factorA" value={customForecastParams.factorA} onChange={handleInputChange} />
            </div>
            <div>
                <label className="m-2">Factor B: </label>
                <input type="number" name="factorB" value={customForecastParams.factorB} onChange={handleInputChange} />
            </div>
            <div>
                <label className="m-2">Factor C: </label>
                <input type="number" name="factorC" value={customForecastParams.factorC} onChange={handleInputChange} />
            </div>
            <button className="btn btn-primary m-2" type="submit">Submit</button>
        </form>;

    return (
        <>
            <h1>Weather Forecasts</h1>
            <h2>Standard Forecast</h2>
            {standardForecastTable}
            <h2>Custom Forecast</h2>
            {customForecastTable}
            <h2>Generate Custom Forecast</h2>
            {customForecastParamsForm}
        </>
    );

    async function populateWeatherData() {
        const standardForecastResponse = await fetch('https://localhost:7272/WeatherForecast/GetNewestStandardForecast');
        if (standardForecastResponse.ok) {
            const data: FiveDayForecast = await standardForecastResponse.json();
            setStandardForecast(data);
        }
        const customForecastResponse = await fetch('https://localhost:7272/WeatherForecast/GetNewestCustomForecast');
        if (customForecastResponse.ok) {
            const data: FiveDayForecast = await customForecastResponse.json();
            setCustomForecast(data);
        }
    }
}

export default App
