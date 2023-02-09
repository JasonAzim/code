using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyAdmin.Model;

namespace PharmacyAdmin.Service
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static string[] CloudCover = new[] {
            "Sunny", "Partly cloudy", "Cloudy", "Storm"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {

            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

        public void Remove(WeatherForecast item)
        {
        }

        public void Update(WeatherForecast dataItem, IDictionary<string, object> newValue)
        {
        }

        public void Insert(IDictionary<string, object> newValue)
        {

        }
    }
}
