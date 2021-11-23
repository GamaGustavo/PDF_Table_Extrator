using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;

namespace PDF_Table_Extrator
{
    [Table("HistoricoAluno")]
    public class HistoricoAluno
    {
        public int Id { get; set; }
        public HistoricoAluno()
        {
            this.DataLeitura = DateTime.Now;
        }
        public List<DisciplinaAluno> Disciplinas { get; protected set; }
       // [System.ComponentModel.DataAnnotations.FileExtensions(Extensions ="pdf")]
        public string FilePath { get; protected set; }
        public DateTime DataLeitura { get; protected set; }
        public DateTime DataArquivo { get; protected set; }
        public Aluno Aluno { get; set; }
        public int AlunoId { get; set; }

        public static HistoricoAluno Parse(string fullFileName)
        {
            var historicoAluno = new HistoricoAluno();

            string historicoEmTXT = Utils.ConvertPdfToText(fullFileName);
            var fileInfo = new FileInfo(fullFileName);
            historicoAluno.Aluno = HistoricoAluno.ExtrairTabelaDadosPessoais(historicoEmTXT);
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
            historicoAluno.DataArquivo = ExtrairDataEmissao(historicoEmTXT).Value;
            return historicoAluno;

        }

        private static DateTime? ExtrairDataEmissao(string historicoEmTXT)
        {
            var end = historicoEmTXT.IndexOf("Dados Pessoais");
            var start = historicoEmTXT.IndexOf("Histórico Escolar - Emitido em");
            var substring = historicoEmTXT.Substring(start, end - start);
            
            foreach (var linha in substring.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (linha.StartsWith("Histórico Escolar - Emitido em", StringComparison.InvariantCultureIgnoreCase))
                {
                    var dataEmissaoTxt  = linha.Split(":",2, StringSplitOptions.TrimEntries)[1].Split("às", StringSplitOptions.TrimEntries);
                    var data = dataEmissaoTxt[0];
                    var hora = dataEmissaoTxt[1];
                    return DateTime.Parse($"{data} {hora}", CultureInfo.CreateSpecificCulture("pt-BR"));
                }
            }
            return null;
        }

        private static Aluno ExtrairTabelaDadosPessoais(string pdfExtarido)
        {
            var start = pdfExtarido.IndexOf("Dados Pessoais");
            var end = pdfExtarido.IndexOf("Dados do Curso");
            var substring = pdfExtarido.Substring(start, end - start);
            var aluno = new Aluno();
            foreach (var linha in substring.Split('\n',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries ))
            {
                if (linha.StartsWith("matr", StringComparison.InvariantCultureIgnoreCase))
                {
                    aluno.Matricula = linha.Split(':', StringSplitOptions.TrimEntries)[1];
                }
                if (linha.StartsWith("nome:", StringComparison.InvariantCultureIgnoreCase))
                {
                    aluno.Nome = linha.Split(':', StringSplitOptions.TrimEntries)[1];
                }
            }
            return aluno;
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