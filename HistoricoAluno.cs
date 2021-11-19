using System;
using System.Collections.Generic;
using System.IO;

namespace PDF_Table_Extrator
{
    public class HistoricoAluno
    {
        public HistoricoAluno()
        {
            this.DataLeitura = DateTime.Now;
        }
        public List<DisciplinaAluno> Disciplinas { get; protected set; }
        public string FilePath { get; protected set; }
        public DateTime DataLeitura { get; protected set; }
        public DateTime DataArquivo { get; protected set; }


        public static HistoricoAluno Parse(string fullFileName)
        {
            var historicoAluno = new HistoricoAluno();
            
            string historico = Utils.ConvertPdfToText(fullFileName);
            var fileInfo = new FileInfo(fullFileName);
            var tabelaHistorico = HistoricoAluno.ExtrairTabelaHistorico(historico);
            string[] linhasBaguncadas = tabelaHistorico.Split("\n");

            var listaDisciplinas = new List<DisciplinaAluno>(linhasBaguncadas.Length - 1);
            for (int i = 0; i < linhasBaguncadas.Length - 1; i++)
            {
                if (linhasBaguncadas[i].Equals("REPROVADO") || linhasBaguncadas[i].Equals("APROVADO POR"))
                {
                    listaDisciplinas.Add(DisciplinaAluno.Parse(linhasBaguncadas[i], linhasBaguncadas[i + 1], linhasBaguncadas[i + 2]));
                    i += 2;
                }
                else
                {
                    string temp = linhasBaguncadas[i].Replace(",", ".");
                    float ch = 0;
                    bool deu = float.TryParse(temp, out ch);
                    if (deu)
                    {
                        listaDisciplinas.Add(DisciplinaAluno.Parse(linhasBaguncadas[i], linhasBaguncadas[++i]));
                    }
                    else
                    {
                        listaDisciplinas.Add(DisciplinaAluno.Parse(temp));
                    }
                }
            }
            historicoAluno.Disciplinas = listaDisciplinas;
            historicoAluno.DataArquivo = fileInfo.CreationTime;
            return historicoAluno;

        }


        private static string ExtrairTabelaHistorico(string pdfExtarido)
        {
            string resultado = null;
            bool linhaValida = false;
            string[] pdfExtaridoVector = pdfExtarido.Split("\n");
            foreach (string linha in pdfExtaridoVector)
            {
                if (linha.StartsWith("Para verificar a autenticidade deste documento", StringComparison.InvariantCultureIgnoreCase))
                {
                    linhaValida = false;
                }
                if (linha.Equals("Legenda", StringComparison.InvariantCultureIgnoreCase))
                {
                    linhaValida = false;
                }
                if (linhaValida)
                {
                    if (!linha.Equals("Letivo", StringComparison.InvariantCultureIgnoreCase))
                    {
                        resultado += linha + "\n";
                    }
                }
                if (linha.Equals("Componente Curricular Quant. Aulas CH Turma Freq % Nota Situação"))
                {
                    linhaValida = true;
                }

            }
            return resultado;
        }
    }
}