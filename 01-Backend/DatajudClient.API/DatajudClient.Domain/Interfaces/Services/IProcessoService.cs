using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Models.Processos;
using System.Linq.Expressions;

namespace DatajudClient.Domain.Interfaces.Services
{
    public interface IProcessoService
    {
        /// <summary>
        /// Inlclui uma lista de processos no banco de dados.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Lista de processos incluídas.</returns>
        Task<RetornoServico<List<ReadProcessoDTO>>> IncluirProcessosAsync(IEnumerable<CreateProcessoDTO> entities);
        /// <summary>
        /// Atualiza as movimentações dos processos informados. 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Lista de processos atualizados</returns>
        Task<RetornoServico<List<ReadProcessoDTO>>> AtualizarProcessosAsync(UpdateProcessoDTO dto);
        /// <summary>
        /// Atualiza os dados do processo informado.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Processo atualizado.</returns>
        Task<RetornoServico<ReadProcessoDTO>> AtualizarDadosProcessoAsync(int id, UpdateDadosProcessoDTO dto);
        /// <summary>
        /// Obtem os processos em que o nome, número do processo ou nome do tribunal contenham a string de busca informada.
        /// </summary>
        /// <param name="busca"></param>
        /// <returns>Lista de processos buscados</returns>
        Task<RetornoServico<List<ReadProcessoDTO>>> ObterProcessosAsync(string busca = "");
        /// <summary>
        /// Obtem o processo com o id informado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<RetornoServico<ReadProcessoDTO>> ObterProcessosPorIdAsync(int id);
        /// <summary>
        /// Exclui os processos informados e todos os seus andamentos.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Numero de registros excluídos</returns>
        Task<RetornoServico<int>> ExcluirProcessos(IEnumerable<int> ids);
    }
}
