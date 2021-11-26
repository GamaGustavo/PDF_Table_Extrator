using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

using historico;

namespace pdfExtrator{
    public static class PdfExtartor{
    
        public static string PdfExtratorGeneric(FileStream historicoPdf){
            PdfReader reader = new PdfReader(historicoPdf);
            PdfDocument pdfDoc = new PdfDocument(reader);
            string resultado = null;
            for(int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                resultado += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
            }
            pdfDoc.Close();
            reader.Close();
            return resultado;
        }

        public static Historico ModelHistoricoIFS(FileStream historicoPdf){
            string pdfExtarido =  PdfExtratorGeneric(historicoPdf);
            Aluno aluno = ExtarctDadosAluno(pdfExtarido);
            List<Disciplina> disciplinas = ExtarctDisciplinas(pdfExtarido);
            string [] pdfExtaridoVector = pdfExtarido.Split("\n");
            string dataCriacao = pdfExtaridoVector[3].Replace("Histórico Escolar - Emitido em: ","");
            Historico his = new Historico(aluno: aluno,disciplinas: disciplinas);
            his.DataArquivo = DateTime.Parse(dataCriacao);
            return his;
        }
        
        public static string ExtarctTableCompCurri(string pdfExtarido){
            string resultado = null;
             bool vai = false;
            string [] pdfExtaridoVector = pdfExtarido.Split("\n");
            foreach(string linha in pdfExtaridoVector){
                if(linha.Equals("Para verificar a autenticidade deste documento entre em  https://sig.ifs.edu.br/sigaa/documentos informando a matrícula, data de")){
                    vai = false;
                }
                if(linha.Equals("Legenda")){
                    vai = false;
                }
                if(vai){
                    if(!linha.Equals("Letivo"))
                    {
                        resultado+= linha+"\n";
                    }
                }
                if(linha.Equals("Componente Curricular Quant. Aulas CH Turma Freq % Nota Situação")){
                    vai = true;
                }
                
            }
            return resultado;
            
        }
        
        public static Aluno ExtarctDadosAluno(string pdfExtarido){
            Aluno aluno = new Aluno();
            string [] pdfExtaridoVector = pdfExtarido.Split("\n");
            foreach(string linha in pdfExtaridoVector){
                if (linha.Contains("Nome:"))
                {
                   aluno.Nome = linha.Replace("Nome:",""); 
                }
                if (linha.Contains("Matrícula:"))
                {
                   aluno.Matricula = linha.Replace("Matrícula:",""); 
                }
                if (aluno.isFullyFull())
                {
                    return aluno;
                }
                
            }
            throw new Exception(message: "os dados do aluno não foram encontrados!");
        }
        public static List<Disciplina> ExtarctDisciplinas(string pdfExtarido){
            string extractTable = ExtarctTableCompCurri(pdfExtarido);
            string [] vetorDeLinhas = extractTable.Split("\n");
            var disciplinas = new List<Disciplina>();
            int countLine =0; 
            while (countLine < vetorDeLinhas.Length-1)
            {
                 if ("REPROVADO".Equals(vetorDeLinhas[countLine])||"APROVADO POR".Equals(vetorDeLinhas[countLine]))
                 {
                    disciplinas.Add(new Disciplina(vetorDeLinhas[countLine],vetorDeLinhas[countLine+1],vetorDeLinhas[countLine+2]));
                     
                    countLine +=3;
                 }else
                 {
                     if ("120,00 REPROVADO".Equals(vetorDeLinhas[countLine]))
                     {
                          disciplinas.Add(new Disciplina(120.00f,"REPROVADO",vetorDeLinhas[countLine+1],vetorDeLinhas[countLine+2]));
                          countLine +=3;
                     }else
                     {
                         if ("120,00".Equals(vetorDeLinhas[countLine]))
                         {
                             disciplinas.Add(new Disciplina(120.00f,vetorDeLinhas[countLine+1]));
                             countLine +=2;
                         }else
                         {
                             disciplinas.Add(new Disciplina(vetorDeLinhas[countLine]));
                             countLine ++;
                         }
                     }
                 }
            }
            return disciplinas;
        }
           
    }
}