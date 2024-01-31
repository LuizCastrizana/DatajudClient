using DatajudClient.Domain.Interfaces.Repositories.Processos;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.Infrastructure.DatabaseContext;

namespace DatajudClient.Infrastructure.Repositories.Processos
{
    public class ProcessoRepository : Repository<Processo>, IProcessoRepository
    {
        public ProcessoRepository(AppDbContext context) : base(context) { }


    }
}
