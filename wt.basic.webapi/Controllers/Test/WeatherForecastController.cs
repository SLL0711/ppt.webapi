using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wt.basic.service.Common;

namespace wt.basic.webapi.Controllers.Test
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiBaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public wtJsonResult json { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, wtJsonResult jsonResult)
        {
            _logger = logger;
            json = jsonResult;
        }

        [HttpGet]
        public void Get()
        {
            _logger.LogInformation("log info");
            var rng = new Random();
            var wArr = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            json.list = wArr;
            json.success = true;
        }
    }
}
