
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;


namespace API_TEMP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        private static List<WeatherForecast> Forecasts = new List<WeatherForecast> {
        new WeatherForecast{Date= DateOnly.FromDateTime(DateTime.Now.AddDays(1)),TemperatureC= 25,Summary = "Warm"},
        new WeatherForecast{Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),TemperatureC = 30,Summary = "Hot"}
        
    };
[HttpGet(Name ="GetWeatherForecast")]
public IEnumerable<WeatherForecast> Get()
{
    return Forecasts;
}
[HttpPost(Name = "CreateWeatherForecast")]
public IActionResult Post([FromBody]WeatherForecast forecast)
{
    Forecasts.Add(forecast);
    return CreatedAtAction(nameof(Get),new {date=forecast.Date},forecast);
}
[HttpPut("{date}",Name ="UpdateWeatherForecast")]
public IActionResult Put(DateOnly date, [FromBody]WeatherForecast updateForecast)
{
    var forecast = Forecasts.FirstOrDefault(f=> f.Date == date); 
    if(forecast == null)
    {
        return NotFound();
    }
    forecast.TemperatureC= updateForecast.TemperatureC; 
    forecast.Summary = updateForecast.Summary;
    return NoContent();
}
[HttpDelete("{date}",Name="DeleteWeatherForecast")]
public IActionResult Delete(DateOnly date)
{
    var forecast= Forecasts.FirstOrDefault(f=> f.Date == date);
    if(forecast == null)
    {
        return NotFound();
    }
    Forecasts.Remove(forecast);
    return NoContent();
}
}
}