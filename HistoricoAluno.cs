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

            string historicoEmTXT = Utils.ConvertPdfToText(fullFileName);
            var fileInfo = new FileInfo(fullFileName);
            var tabelaHistorico = HistoricoAluno.ExtrairTabelaHistorico(historicoEmTXT);

            string[] linhaTabelaHistorico = tabelaHistorico.Split("\n");

            var listaDisciplinas = new List<DisciplinaAluno>(linhaTabelaHistorico.Length - 1);
            for (int i = 0; i < linhaTabelaHistorico.Length - 1; i++)
            {
                if (linhaTabelaHistorico[i].Equals("REPROVADO", StringComparison.InvariantCultureIgnoreCase) ||
                    linhaTabelaHistorico[i].Equals("APROVADO POR", StringComparison.InvariantCultureIgnoreCase)
                )
                {
                    listaDisciplinas.Add(DisciplinaAluno.Parse(linhaTabelaHistorico[i], linhaTabelaHistorico[i + 1], linhaTabelaHistorico[i + 2]));
                    i += 2;
                }
                else
                {
                    string temp = linhaTabelaHistorico[i].Replace(",", ".");
                    float ch = 0;

                    if (float.TryParse(temp, out ch))
                    {
                        listaDisciplinas.Add(DisciplinaAluno.Parse(linhaTabelaHistorico[i], linhaTabelaHistorico[++i]));
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