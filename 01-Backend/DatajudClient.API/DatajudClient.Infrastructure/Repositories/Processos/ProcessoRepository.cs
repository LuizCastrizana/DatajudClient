using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Models.Endereco;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.Domain.Models.Tribunais;
using DatajudClient.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatajudClient.Infrastructure.Repositories
{
    public class ProcessoRepository : Repository<Processo>, IProcessoRepository
    {
        public ProcessoRepository(AppDbContext context) : base(context) { }

        public override List<Processo> Obter(Expression<Func<Processo, bool>> predicate)
        {
            return _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Estado)
                .Include(x => x.Tribunal).ThenInclude(x => x.Categoria)
                .Include(x => x.Tribunal).ThenInclude(x => x.Estado)
                .Include(x => x.Andamentos).ThenInclude(x => x.Complementos)
                .ToList();
        }

        public override async Task<List<Processo>> ObterAsync(Expression<Func<Processo, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Estado)
                .Include(x => x.Tribunal).ThenInclude(x => x.Categoria)
                .Include(x => x.Tribunal).ThenInclude(x => x.Estado)
                .Include(x => x.Andamentos).ThenInclude(x => x.Complementos)
                .ToListAsync();
        }

        public override int Adicionar(Processo obj)
        {
            obj.Tribunal = _dbContext.Find<Tribunal>(obj.TribunalId);
            obj.Estado = _dbContext.Find<Estado>(obj.EstadoId);

            if (obj.Tribunal == null)
                throw new Exception("Tribunal não encontrado.");
            if (obj.Estado == null)
                throw new Exception("Estado não encontrado.");

            _dbSet.Add(obj);

            return _dbContext.SaveChanges();
        }

        public override async Task<int> AdicionarAsync(Processo obj)
        {
            obj.Tribunal = await _dbContext.FindAsync<Tribunal>(obj.TribunalId);
            obj.Estado = await _dbContext.FindAsync<Estado>(obj.EstadoId);

            if (obj.Tribunal == null)
                throw new Exception("Tribunal não encontrado.");
            if (obj.Estado == null)
                throw new Exception("Estado não encontrado.");

            _dbSet.Add(obj);
            
            return await _dbContext.SaveChangesAsync();
        }
    }
}
