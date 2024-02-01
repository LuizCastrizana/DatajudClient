using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatajudClient.Infrastructure.Repositories.Processos
{
    public class ProcessoRepository : Repository<Processo>, IProcessoRepository
    {
        public ProcessoRepository(AppDbContext context) : base(context) { }

        public override List<Processo> Obter(Expression<Func<Processo, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Andamentos).ThenInclude(x => x.Complementos)
                .ToList();
        }
    }
}
