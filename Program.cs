using System;
using System.Linq;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;


namespace PDF_Table_Extrator
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var arquivos = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Data"), "*.pdf");
            foreach (var file in arquivos)
            {
                var historico = HistoricoAluno.Parse(file);
                PrintTableAluno(historico);

            }

            var arquivo = Path.Combine(Environment.CurrentDirectory, "Data",
                "historico_2019000813_gustavo.pdf");
            
            ///Exibe os dados salvos na lista de CompCurri(Componente curricular).
            //GravarNoBancoDados(historico);



        }

        private static void PrintTableAluno(HistoricoAluno historico)
        {
            Console.WriteLine($"Histórico de {historico.Aluno.Nome} ({historico.Aluno.Matricula})");
            PrintTableDisciplinas(historico.Disciplinas.ToArray());
        }

        private static void AtualizarNotasDe(int notaAtual, int novaNota)
        {
            using (var db = new HistoricoDB())
            {
                var consulta = db.DisciplinasDosAlunos.Where(
                    d => d.HistoricoAlunoId == 1 &&
                    d.Nota >= notaAtual
                );
                var dados = consulta.ToList();
                dados.ForEach(d => d.Nota = novaNota);
                db.SaveChanges(true);

            }
        }


        private static void ExibirNotasAcimaDe(int nota)
        {
            using (var db = new HistoricoDB())
            {
                var consulta = db.DisciplinasDosAlunos.Where(
                    d => d.HistoricoAlunoId == 1 &&
                    d.Nota >= nota
                );
                Console.WriteLine($"A quantidade encontrada foi de {consulta.Count()}");
                var resultado = consulta.ToArray();
                //PrintTable(resultado);

            }

        }

        private static void GravarNoBancoDados(HistoricoAluno historico)
        {
            int qtd = 0;
            using (var bd = new HistoricoDB())
            {
                bd.HistoricoDeAlunos.Add(historico);
                historico.Disciplinas.ForEach(d => bd.DisciplinasDosAlunos.Add(d));
                qtd = bd.SaveChanges();
            }
            Console.WriteLine($"Incluídas {qtd} disciplinas.");
        }

        public static void PrintTableDisciplinas(params DisciplinaAluno[] disciplinasAluno)
        {

            foreach (var item in disciplinasAluno)
            {
                if (item == null)
                    break;
                string linha = null, temp = item.ToString();
                for (int i = 0; i < temp.Length; i++)
                {
                    linha += "-";
                }
                Console.WriteLine("-".PadLeft(temp.Length, '-'));
                Console.WriteLine();
                Console.WriteLine(temp);
            }
            Console.WriteLine("-".PadLeft(150, '-'));
        }
    }
}