using System;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;


namespace PDF_Table_Extrator
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //  Console.WriteLine(ExtarctPdf("historico_2019000813.pdf"));
            ///Exibe os dados salvos na lista de CompCurri(Componente curricular).
            PrintTable(HistoricoAluno.Parse("Nome do documento").Disciplinas.ToArray());
        }



        public static void PrintTable(params DisciplinaAluno[] disciplinasAluno)
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
                Console.WriteLine("-".PadLeft(temp.Length));
                Console.WriteLine();
                Console.WriteLine(temp);
            }
            Console.WriteLine("-".PadLeft(150));
        }
    }
}