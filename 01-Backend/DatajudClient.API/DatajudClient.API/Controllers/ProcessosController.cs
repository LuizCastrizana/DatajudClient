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
        public IActionResult Post([FromBody] IEnumerable<CreateProcessoDTO> dtos) => 
            this.TratarRespostaServico(_processoService.IncluirProcessos(dtos));

        [HttpPost("AtualizarProcessos")]
        public IActionResult Put([FromBody] UpdateProcessoDTO dto) => 
            this.TratarRespostaServico(_processoService.AtualizarProcessos(dto));
    }
}
