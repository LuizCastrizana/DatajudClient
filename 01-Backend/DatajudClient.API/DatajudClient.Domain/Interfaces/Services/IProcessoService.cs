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
        /// <summary>
        /// Atualiza as movimentações dos processos informados. 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Lista de processos atualizados</returns>
        RetornoServico<List<ReadProcessoDTO>> AtualizarProcessos(UpdateProcessoDTO dto);
    }
}
