using DatajudClient.Domain.Models.Processos;
using System.Linq.Expressions;

namespace DatajudClient.Domain.Interfaces.Repositories.Processos
{
    public interface IProcessoRepository : IRepository<Processo>
    {
        /// <summary>
        /// Obtém processos com base em um predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Lista de entidades buscadas.</returns>
        List<Processo> Obter(Expression<Func<Processo, bool>> predicate);
    }
}
