using System;
using System.Collections;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

using historico;

namespace pdfExtrator{
    public class pdfExtartor{
        
        
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

        public Historico ModelHistoricoIFS(FileStream historicoPdf){
            string pdfExtarido =  PdfExtratorGeneric(historicoPdf);
            Aluno aluno = ExtarctDadosAluno(pdfExtarido);
            ArrayList disciplinas = ExtarctDisciplinas(pdfExtarido);
            return new Historico(aluno: aluno,disciplinas: disciplinas);
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
            return new Aluno();
        }
        public static ArrayList ExtarctDisciplinas(string pdfExtarido){
            string extractTable = ExtarctTableCompCurri(pdfExtarido);
            string [] vetorDeLinhas = extractTable.Split("\n");
            var disciplinas = new ArrayList();
            int countLine =0; 
            while (countLine <= vetorDeLinhas.Length)
            {
                 if ("REPROVADO".Equals(vetorDeLinhas[countLine])||"APROVADOR POR".Equals(vetorDeLinhas[countLine]))
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