using System;

public class WeatherInfo.Entities.WeatherForecast
{
    public string Name { get; set; }

    public IEnumerable<WeatherInfo> WeatherInfo { get; set; }

    public Temperature Main { get; set; }
}
public class WeatherInfo
{
    public string Main { get; set; }
    public string Description { get; set; }
}

public class Temperature
{
    public string Temp { get; set; }
}
