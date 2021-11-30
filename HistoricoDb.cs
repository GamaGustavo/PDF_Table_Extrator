using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace historico{
    public class HistoricoDb :DbContext{
        public DbSet<Historico> HistoricosAlunos { get; set; }
        public DbSet<Disciplina> DisciplinasAlunos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=.;Database=csustenidodb;Username=postgres;Password=mysecretepassword");

    }
}