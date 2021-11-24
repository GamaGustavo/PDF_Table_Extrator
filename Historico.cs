using System;
using System.IO;
using System.Collections.Generic;
using pdfExtrator;

namespace historico{
    public class Historico{
        public int Id { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
        public Aluno Aluno{get ; set;}
        public DateTime DataLeitura { get; set; }
        public DateTime DataArquivo { get; set; }
        
        public Historico(List<Disciplina> disciplinas, Aluno aluno){
            this.Disciplinas  = disciplinas;
            this.Aluno = aluno;
            this.DataLeitura = DateTime.Now;
        }
        public Historico()
        {
            this.Disciplinas  = new List<Disciplina>();
            this.Aluno = new Aluno();
            this.DataLeitura = DateTime.Now;
        }
        public static Historico Parse(FileStream fileStream)
        {
            return PdfExtartor.ModelHistoricoIFS(fileStream);
        }
        public static Historico Parse(string filePath)
        {
            FileStream fileStream = new FileStream(path:filePath , mode:FileMode.Open);
            return PdfExtartor.ModelHistoricoIFS(fileStream);
        }
    }
    
}