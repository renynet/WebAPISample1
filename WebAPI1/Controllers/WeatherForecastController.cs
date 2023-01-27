using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI1;

namespace WebAPI1.Controllers
{
    [ApiController]
    //[System.Web.Http.RoutePrefix("api/EmployeeApi")]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private const double V = (5.0 / 9.0);
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/GetCentigrade")]
        public double GetCentigrade(double farenhight)
        {

            return (farenhight - 32.0) * V;
        }

        [HttpGet("/Fareheight")]
        public double Fareheight(double centigrade)
        {

            return (centigrade * 1.8) + 32.0;
        }

        [HttpGet("/Divide")]
        public double Divide(int A, int B)
        {

            return A / B;
        }
        // GET another/5
        [HttpGet("/another/{id}")]
        public string AnotherOne(int id)
        {
            return "request for AnotherOne method with id:" + id;
        }
        // [Route("GetCentigrade/{farenhight}")]
        //[HttpPost(Name = "GetDecCentigrade")]
        /*  public decimal GetDecCentigrade(decimal farenhight, int i)
          {

              return (farenhight - 32) * (decimal)V;
          }*/

        /* [HttpGet(Name = "PutWeatherForecast")]
         public IEnumerable<WeatherForecast> GetCent()
         {
             return Enumerable.Range(1, 5).Select(index => new WeatherForecast
             {
                 Date = DateTime.Now.AddDays(index),
                 TemperatureC = Random.Shared.Next(-20, 55),
                 Summary = Summaries[Random.Shared.Next(Summaries.Length)]
             })
             .ToArray();
         }*/

    }
}