using DatajudClient.Domain.Interfaces.Services.Processos;
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
    }
}
