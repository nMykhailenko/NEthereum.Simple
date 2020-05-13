using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NEthereum.Simple.BLL.Services.Ethereum.Contracts;
using NEthereum.Simple.BLL.Services.Ethereum.ContractsDeployment;

namespace NEthereum.Simple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(
            ILogger<BlockchainController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Blockchain(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Blockchain([FromBody]object obj)
        {
            return Ok();
        }
    }
}
