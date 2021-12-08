using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Table_Extrator
{
    public class CarregarHistoricoServices
    {
        public CarregarHistoricoServices(IConfiguration configuration, HistoricoDBContext context, ILogger<CarregarHistoricoServices> logger)
        {
            this.Configuration = configuration;
            this.context = context;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        private readonly HistoricoDBContext context;

        public ILogger logger { get; }

        public int AtualizarNotasDe(int notaAtual, int novaNota, int aluno = 1)
        {
            var consulta = context.DisciplinasDosAlunos.Where(
                d => d.HistoricoAlunoId == aluno &&
                d.Nota >= notaAtual
            );
            var dados = consulta.ToList();
            dados.ForEach(d => d.Nota = novaNota);
            return context.SaveChanges(true);

        }
        public IEnumerable<DisciplinaAluno> ObterNotasAcimaDe(int nota, int aluno = 1)
        {

            var consulta = context.DisciplinasDosAlunos.Where(
                d => d.HistoricoAlunoId == aluno &&
                d.Nota >= nota
            );
            logger.LogInformation($"A quantidade encontrada foi de {consulta.Count()}");
            return consulta.ToArray();
        }

        public int GravarNoBancoDados(params HistoricoAluno[] historicos)
        {
            int qtd = 0;
            foreach (var historico in historicos)
            {
                context.HistoricoDeAlunos.Add(historico);
                historico.Disciplinas.ForEach(d => context.DisciplinasDosAlunos.Add(d));
            }

            qtd = context.SaveChanges();

            logger.LogInformation($"Incluídas {qtd} disciplinas para {historicos.Length} históricos de alunos.");

            return qtd;
        }
       

        public HistoricoAluno CarregarArquivoExemplo()
        {
            var dataPath = this.Configuration.GetValue<string>("DataPath");
            dataPath = string.IsNullOrWhiteSpace(dataPath) ? Path.Combine(Environment.CurrentDirectory, "Data") : dataPath;
            var fileSample = this.Configuration.GetValue<string>("ArquivoExemplo");
            var arquivo = Path.Combine(dataPath, fileSample);
            return CarregarHistorico(arquivo);
        }

        public HistoricoAluno CarregarHistorico(string file)
        {
            var historico = HistoricoAluno.Parse(file);
            return historico;
        }
        public IEnumerable<HistoricoAluno> CarregarArquivosHistoricos()
        {
            var dataPath = this.Configuration.GetValue<string>("DataPath");
            dataPath = string.IsNullOrWhiteSpace(dataPath) ? Path.Combine(Environment.CurrentDirectory, "Data") : dataPath;
            var arquivos = Directory.GetFiles(dataPath, "*.pdf");            
            foreach (var file in arquivos)
            {
                yield return CarregarHistorico(file);
            }
            ///Exibe os dados salvos na lista de CompCurri(Componente curricular).
            
        }

    }
}
