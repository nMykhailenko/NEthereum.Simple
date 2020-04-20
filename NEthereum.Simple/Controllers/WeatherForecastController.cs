using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NEthereum.Simple.Controllers
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

        [Route("/blockchain")]
        [HttpGet]
        public async Task<IActionResult> Blockchain()
        {
            var service = new Blockchain();
            var t = await service.CreateRespone("{\"id\":\"2\"}", "getMaterial");
            return Ok(t);
        }

        [Route("/blockchain/add")]
        [HttpGet]
        public async Task<IActionResult> BlockchainAdd()
        {
            var service = new Blockchain();
            var body = "{\"id\":\"2\",\"idBioRefMaterial\":\"ref 2\",\"name\":\"name 2\",\"unit\":\"1\",\"isBio\":0,\"dateCreated\":\"123\"}";
            var t = await service.CreateRespone(body, "addMaterial", true);
            return Ok(t);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
