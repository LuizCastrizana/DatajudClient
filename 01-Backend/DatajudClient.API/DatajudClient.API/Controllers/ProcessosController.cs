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
    }
}
