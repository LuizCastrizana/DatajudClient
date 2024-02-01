using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;

namespace DatajudClient.Domain.Interfaces.Services
{
    public interface IProcessoService
    {
        /// <summary>
        /// Inlclui uma lista de processos no banco de dados.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Lista de processos incluídas.</returns>
        RetornoServico<List<ReadProcessoDTO>> IncluirProcessos(IEnumerable<CreateProcessoDTO> entities);
    }
}
