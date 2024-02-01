using DatajudClient.Domain.Models;
using System.Linq.Expressions;

namespace DatajudClient.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : ModelBase
    {
        /// <summary>
        /// Inclui um objeto no banco de dados.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Número de registros afetados.</returns>
        int Adicionar(T obj);
        /// <summary>
        /// Inclui um objeto no banco de dados.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Número de registros afetados.</returns>
        Task<int> AdicionarAsync(T obj);
        /// <summary>
        /// Obtem uma lista de objetos de acordo com o predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Lista de objetos.</returns>
        List<T> Obter(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Obtem uma lista de objetos de acordo com o predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Lista de objetos.</returns>
        Task<List<T>> ObterAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Exclui um objeto do banco de dados.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Número de registros excluidos.</returns>
        int Excluir(T obj, bool exclusaoFisica = false);
        /// <summary>
        /// Exclui um objeto do banco de dados.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Número de registros excluidos.</returns>
        Task<int> ExcluirAsync(T obj, bool exclusaoFisica = false);
        /// <summary>
        /// Salva as alterações no banco de dados.
        /// </summary>
        /// <returns>Número de registros afetados.</returns>
        int SalvarAlteracoes();
        /// <summary>
        /// Salva as alterações no banco de dados.
        /// </summary>
        /// <returns>Número de registros afetados.</returns>
        Task<int> SalvarAlteracoesAsync();
    }
}
