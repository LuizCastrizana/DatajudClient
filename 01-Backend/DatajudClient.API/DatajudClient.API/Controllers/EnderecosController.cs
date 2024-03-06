using DatajudClient.API.Extensions;
using DatajudClient.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatajudClient.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecosController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet("ObterEstados")]
        public async Task<IActionResult> GetEstados([FromQuery] string? busca) =>
            this.TratarRespostaServico(await _enderecoService.ObterEstadosAsync(busca ??= string.Empty));
    }
}
