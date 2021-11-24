using System.ComponentModel.DataAnnotations;

namespace historico{
    public class Aluno{
        public string Nome { get; set; }
        [Key]
        public string Matricula { get; set; }    
        public Aluno(){}
        public Aluno(string nome,string matricula){
            this.Nome = nome;
            this.Matricula = matricula;
        }
    }
}