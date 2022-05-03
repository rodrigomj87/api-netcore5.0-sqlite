using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testedev.Models;

namespace testedev.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Conteiner> Conteiners { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoMovimentacao> TipoMovimentacao { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>().HasData(
                new Clientes { Id = 1, NmCliente = "João Pablo"},
                new Clientes { Id = 2, NmCliente = "Maria Cardoso" },
                new Clientes { Id = 3, NmCliente = "Adalberto Penna" }
                );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, NmStatus = "Vazio" },
                new Status { Id = 2, NmStatus = "Cheio" }
                );

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, NmCategoria = "Importação" },
                new Categoria { Id = 2, NmCategoria = "Exportação" }
                );

            modelBuilder.Entity<TipoMovimentacao>().HasData(
                new TipoMovimentacao { Id = 1, NmTipoMovimentacao = "Embarque" },
                new TipoMovimentacao { Id = 2, NmTipoMovimentacao = "Descarga" },
                new TipoMovimentacao { Id = 3, NmTipoMovimentacao = "Gate In" },
                new TipoMovimentacao { Id = 4, NmTipoMovimentacao = "Gate Out" },
                new TipoMovimentacao { Id = 5, NmTipoMovimentacao = "Reposicionamento" },
                new TipoMovimentacao { Id = 6, NmTipoMovimentacao = "Pesagem" },
                new TipoMovimentacao { Id = 7, NmTipoMovimentacao = "Scanner" }
                );
        }
    }
}
