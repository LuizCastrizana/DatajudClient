using DatajudClient.Domain.Interfaces.Repositories.Processos;
using DatajudClient.Domain.Interfaces.Services.Processos;

namespace DatajudClient.Domain.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;   

        public ProcessoService(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }
    }
}
