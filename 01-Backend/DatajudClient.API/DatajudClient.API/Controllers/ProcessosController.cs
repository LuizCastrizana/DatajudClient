using DatajudClient.API.Extensions;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatajudClient.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessosController : ControllerBase
    {
        private readonly IProcessoService _processoService;

        public ProcessosController(IProcessoService processoService)
        {
            _processoService = processoService;
        }

        [HttpPost("CadastrarProcessos")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CreateProcessoDTO> dtos) => 
            this.TratarRespostaServico(await _processoService.IncluirProcessosAsync(dtos));

        [HttpPost("AtualizarProcessos")]
        public async Task<IActionResult> Put([FromBody] UpdateProcessoDTO dto) => 
            this.TratarRespostaServico(await _processoService.AtualizarProcessosAsync(dto));

        [HttpPut("AtualizarDadosProcesso/{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateDadosProcessoDTO dto) => 
            this.TratarRespostaServico(await _processoService.AtualizarDadosProcessoAsync(id, dto));

        [HttpGet("ObterProcessos")]
        public async Task<IActionResult> Get([FromQuery] string? busca) => 
            this.TratarRespostaServico(await _processoService.ObterProcessosAsync(busca ??= string.Empty));

        [HttpGet("ObterProcesso/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) => 
            this.TratarRespostaServico(await _processoService.ObterProcessosPorIdAsync(id));

        [HttpPost("ExcluirProcessos")]
        public async Task<IActionResult> Delete([FromBody] DeleteProcessoDTO dto) => 
            this.TratarRespostaServico(await _processoService.ExcluirProcessos(dto.Ids));
    }
}
