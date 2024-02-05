using DatajudClient.Domain.Models.Tribunais;
using System.Linq.Expressions;

namespace DatajudClient.Domain.Interfaces.Repositories
{
    public interface ITribunalRepository : IRepository<Tribunal>
    {
        List<Tribunal> Obter(Expression<Func<Tribunal, bool>> predicate);
    }
}
