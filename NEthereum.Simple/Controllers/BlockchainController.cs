using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NEthereum.Simple.Models;
using NEthereum.Simple.Models.Base;

namespace NEthereum.Simple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockchainController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(ILogger<BlockchainController> logger)
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

        [Route("~/[controller]/add")]
        [HttpGet]
        public async Task<IActionResult> BlockchainAdd([FromBody]BlockModel model)
        {
            var service = new Blockchain();
            var result = await service.CreateAsync(model);

            return Ok(result);
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
