using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PDF_Table_Extrator
{
    public class HistoricoDB: DbContext
    {

        public DbSet<HistoricoAluno> HistoricoDeAlunos { get; set; }
        public DbSet<DisciplinaAluno> DisciplinasDosAlunos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=Exemplo2021_2;Trusted_Connection=False;User Id=sa; password=P@ssw0rd");
        }
    }
}
