using DatajudClient.Domain.Models;
using DatajudClient.Domain.Models.Processos;
using Microsoft.EntityFrameworkCore;

namespace DatajudClient.Infrastructure.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        #region DbSet Processos

        public DbSet<Processo> Processo { get; set; }
        public DbSet<AndamentoProcesso> AndamentoProcesso { get; set; }
        public DbSet<ComplementoAndamento> ComplementoAndamento { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Processos

            builder.Entity<Processo>()
                .HasMany(x=>x.Andamentos)
                .WithOne(x=>x.Processo)
                .HasForeignKey(x=>x.ProcessoId);

            builder.Entity<AndamentoProcesso>()
                .HasMany(x=>x.Complementos)
                .WithOne(x=>x.AndamentoProcesso)
                .HasForeignKey(x=>x.AndamentoProcessoId);

            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetModelBase();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetModelBase();
            return base.SaveChanges();
        }

        #region Métodos privados

        private void SetModelBase()
        {
            var entidadesCriadas = this.ChangeTracker.Entries()
                                   .Where(x => x.State == EntityState.Added)
                                   .Select(x => x.Entity);

            foreach (var item in entidadesCriadas)
            {
                var herdaModelBase = item as ModelBase;
                if (herdaModelBase != null)
                {
                    herdaModelBase.DataInclusao = DateTime.Now;
                    herdaModelBase.Ativo = true;
                }
            }

            var entidadesAtualizadas = this.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var item in entidadesAtualizadas)
            {
                var herdaModelBase = item as ModelBase;
                if (herdaModelBase != null)
                {
                    herdaModelBase.DataAlteracao = DateTime.Now;
                }
            }
        }

        #endregion
    }
}
