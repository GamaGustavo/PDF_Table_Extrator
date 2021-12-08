using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace PDF_Table_Extrator
{
    public class HistoricoDBContext: DbContext
    {
        
        public DbSet<HistoricoAluno> HistoricoDeAlunos { get; set; }
        public DbSet<DisciplinaAluno> DisciplinasDosAlunos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }

        public HistoricoDBContext (DbContextOptions<HistoricoDBContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
