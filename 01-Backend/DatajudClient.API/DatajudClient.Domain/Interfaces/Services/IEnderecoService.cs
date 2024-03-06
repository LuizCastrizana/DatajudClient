using DatajudClient.Domain.DTO.Enderecos;
using DatajudClient.Domain.DTO.Shared;

namespace DatajudClient.Domain.Interfaces.Services
{
    public interface IEnderecoService
    {
        /// <summary>
        /// Obtem os estados em que o nome ou UF contenham a string de busca informada.
        /// </summary>
        /// <param name="busca"></param>
        /// <returns>Lista de estados buscados</returns>
        Task<RetornoServico<List<ReadEstadoDTO>>> ObterEstadosAsync(string busca = "");
    }
}
