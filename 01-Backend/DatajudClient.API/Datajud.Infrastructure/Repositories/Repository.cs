using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Models;
using DatajudClient.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatajudClient.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public virtual int Adicionar(T obj)
        {
            _dbSet.Add(obj);
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> AdicionarAsync(T obj)
        {
            _dbSet.Add(obj);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual IEnumerable<T> Obter(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(x => x.Ativo);
        }

        public virtual async Task<List<T>> ObterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).Where(x => x.Ativo).ToListAsync();
        }

        public virtual int Excluir(T obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _dbSet.Attach(obj);
                _dbSet.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> ExcluirAsync(T obj, bool exclusaoFisica = false)
        {
            if (exclusaoFisica)
            {
                _dbSet.Attach(obj);
                _dbSet.Remove(obj);
            }
            else
            {
                obj.Ativo = false;
            }

            return await _dbContext.SaveChangesAsync();
        }

        public virtual int SalvarAlteracoes()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> SalvarAlteracoesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
