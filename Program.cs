using System;
using System.IO;
using historico;
namespace pdfExtrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // FileStream file = new FileStream(path:"/home/gustavo/Code/historico_2019000813.pdf",mode:FileMode.Open);
            //  Console.WriteLine(PdfExtartor.PdfExtratorGeneric(file));
            // Historico his = PdfExtartor.ModelHistoricoIFS(file);
            // Console.WriteLine(his.Disciplinas[0] as Disciplina);

            ///Exibe os dados salvos na lista de CompCurri(Componente curricular).
            GravarNoBancoDados(Historico.Parse("/home/gustavo/Code/historico_2019000813.pdf"));
        }
        private static void GravarNoBancoDados(Historico historico)
        {
            int qtd = 0;
            using (var bd = new HistoricoDb())
            {
                bd.HistoricosAlunos.Add(historico);
                historico.Disciplinas.ForEach(d => bd.DisciplinasAlunos.Add(d));
                qtd = bd.SaveChanges();
            }
            Console.WriteLine($"Incluídas {qtd} disciplinas.");
        }
    }     
}
