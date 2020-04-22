using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NEthereum.Simple.Models;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Blockchain(int id)
        {
            var service = new Blockchain();
            var result = await service.GetAsync(id);

            return Ok(result);
        }

        //[Route("/blockchain/add")]
        //[HttpGet]
        //public async Task<IActionResult> BlockchainAdd()
        //{
        //    var model = new BlockModel
        //    {
        //        dateCreated = 333,
        //        id = 3,
        //        idBioRefMaterial = "ref 3",
        //        name = "name 3",
        //        isBio = 1,
        //        unit = 2
        //    };
        //    var service = new Blockchain();
        //    var result = await service.CreateAsync(model);

        //    return Ok(result);
        //}

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
