using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Models.Tribunais;
using DatajudClient.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatajudClient.Infrastructure.Repositories
{
    public class TribunalRepository : Repository<Tribunal>, ITribunalRepository
    {
        public TribunalRepository(AppDbContext context) : base(context) { }

        public override List<Tribunal> Obter(Expression<Func<Tribunal, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Categoria)
                .ToList();
        }

        public override async Task<List<Tribunal>> ObterAsync(Expression<Func<Tribunal, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Categoria)
                .ToListAsync();
        }
    }
}
