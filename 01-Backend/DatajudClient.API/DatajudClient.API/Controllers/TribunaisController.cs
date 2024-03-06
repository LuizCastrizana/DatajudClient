using DatajudClient.API.Extensions;
using DatajudClient.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatajudClient.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TribunaisController : ControllerBase
    {
        private readonly ITribunalService _tribunalService;

        public TribunaisController(ITribunalService tribunalService)
        {
            _tribunalService = tribunalService;
        }

        [HttpGet("ObterTribunais")]
        public async Task<IActionResult> Get([FromQuery] string? busca) =>
            this.TratarRespostaServico(await _tribunalService.ObterTribunaisAsync(busca ??= string.Empty));

        [HttpGet("ObterTribunal")]
        public async Task<IActionResult> Get([FromQuery] int id) =>
            this.TratarRespostaServico(await _tribunalService.ObterTribunalPorIdAsync(id));
    }
}
