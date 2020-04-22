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

        [Route("/blockchain/add")]
        [HttpGet]
        public async Task<IActionResult> BlockchainAdd()
        {
            var model = new BlockModelRequest
            {
                dateCreated = 333,
                id = 3,
                idBioRefMaterial = "ref 3",
                name = "name 3",
                isBio = 1,
                unit = 2
            };
            var service = new Blockchain();
            await service.CommandAsync(model, "addMaterial");

            return Ok();
        }
    }
}
