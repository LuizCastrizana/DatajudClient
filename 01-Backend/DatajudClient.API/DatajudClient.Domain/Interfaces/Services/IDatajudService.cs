using DatajudClient.Domain.DTO.Datajud;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Models.Processos;

namespace DatajudClient.Domain.Interfaces.Services
{
    public interface IDatajudService
    {
        /// <summary>
        /// Obtém os dados do processo no Datajud.
        /// </summary>
        /// <param name="processo"></param>
        /// <returns></returns>
        Task<RetornoServico<ResponseDatajudDTO>> ObterDadosProcessoAsync(Processo processo);
    }
}
