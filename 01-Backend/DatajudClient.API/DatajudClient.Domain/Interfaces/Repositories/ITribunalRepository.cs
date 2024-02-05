using DatajudClient.Domain.Models.Tribunais;
using System.Linq.Expressions;

namespace DatajudClient.Domain.Interfaces.Repositories
{
    public interface ITribunalRepository : IRepository<Tribunal>
    {
        /// <summary>
        /// Obtém os tribunais cadastrados no banco de dados de acordo com o predicado informado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Lista de tribunais.</returns>
        List<Tribunal> Obter(Expression<Func<Tribunal, bool>> predicate);
        /// <summary>
        /// Obtém de forma assíncrona os tribunais cadastrados no banco de dados de acordo com o predicado informado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Lista de tribunais.</returns>
        Task<List<Tribunal>> ObterAsync(Expression<Func<Tribunal, bool>> predicate);
    }
}
