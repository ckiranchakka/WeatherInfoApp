using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherInfo.Entities
{
    public class WeatherViewModel
    {
        public List<Forecast> forecasts { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
    }
    public class Forecast
    {
        public Temperature Temperature { get; set; }
        public string Date { get; set; }
        public DateTime Date_time { get; set; }
        public string Day { get; set; }
        public double Pressure { get; set; }
        public double Humdity { get; set; }

        public double Speed { get; set; }
        public string Description { get; set; }
        public string WeatherType { get; set; }

        public string Icon { get; set; }

    }
    public class Temperature
    {
        public double CelsiusCurrent { get; set; }
        public double FahrenheitCurrent { get; set; }
        public double KelvinCurrent { get; set; }
        public double CelsiusMinimum { get; set; }
        public double CelsiusMaximum { get; set; }
        public double FahrenheitMinimum { get; set; }
        public double FahrenheitMaximum { get; set; }
        public double KelvinMinimum { get; set; }
        public double KelvinMaximum { get; set; }

        public Temperature(double temp, double min, double max)
        {
            KelvinCurrent = temp;
            KelvinMaximum = max;
            KelvinMinimum = min;

            CelsiusCurrent = convertToCelsius(KelvinCurrent);
            CelsiusMaximum = convertToCelsius(KelvinMaximum);
            CelsiusMinimum = convertToCelsius(KelvinMinimum);

            FahrenheitCurrent = convertToFahrenheit(CelsiusCurrent);
            FahrenheitMaximum = convertToFahrenheit(CelsiusMaximum);
            FahrenheitMinimum = convertToFahrenheit(CelsiusMinimum);
        }

        private double convertToFahrenheit(double celsius)
        {
            return Math.Round(((9.0 / 5.0) * celsius) + 32, 3);
        }

        private double convertToCelsius(double kelvin)
        {
            return Math.Round(kelvin - 273.15, 3);
        }
    }
}
