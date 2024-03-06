using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.DTO.Tribunais;

namespace DatajudClient.Domain.Interfaces.Services
{
    public interface ITribunalService
    {
        /// <summary>
        /// Obtem os tribunais em que o nome, número do processo ou nome do tribunal contenham a string de busca informada.
        /// </summary>
        /// <param name="busca"></param>
        /// <returns>Lista de tribunais buscados</returns>
        Task<RetornoServico<List<ReadTribunalDTO>>> ObterTribunaisAsync(string busca = "");
        /// <summary>
        /// Obtem os dados de um tribunal por seu id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tribunal buscado</returns>
        Task<RetornoServico<ReadTribunalDTO>> ObterTribunalPorIdAsync(int id);
    }
}
