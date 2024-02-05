using DatajudClient.Domain.DTO.Datajud;
using DatajudClient.Domain.Models.Processos;

namespace DatajudClient.Domain.Interfaces.Services
{
    public interface IDatajudService
    {
        ResponseDatajudDTO ObterDadosProcesso(Processo processo);
    }
}
