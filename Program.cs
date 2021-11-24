using System;
using System.IO;
using historico;
namespace pdfExtrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileStream file = new FileStream(path:"/home/gustavo/Code/historico_2019000813.pdf",mode:FileMode.Open);
             Console.WriteLine(PdfExtartor.PdfExtratorGeneric(file));
            // Historico his = PdfExtartor.ModelHistoricoIFS(file);
            // Console.WriteLine(his.Disciplinas[0] as Disciplina);
        }
    }     
}
