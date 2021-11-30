using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace historico{
    public class Aluno{
        [Required]
        public string Nome { get; set; }
        [Key][Required]
        public string Matricula { get; set; }    
        public Aluno(){}
        public Aluno(string nome,string matricula){
            this.Nome = nome;
            this.Matricula = matricula;
        }
        public int HistoricoId { get; set; }

        public bool isPartialEmpty(){
            bool nomeIsEmpty = (Nome == null || "".Equals(Nome));
            bool matriculaIsEmpty = (Matricula == null || "".Equals(Matricula));

            return nomeIsEmpty || matriculaIsEmpty;
        }
        public bool isFullyEmpty(){
            bool nomeIsEmpty = (Nome == null || "".Equals(Nome));
            bool matriculaIsEmpty = (Matricula == null || "".Equals(Matricula));

            return nomeIsEmpty && matriculaIsEmpty;
        }
        public bool isFullyFull(){
            bool nomeIsEmpty = (Nome != null && !"".Equals(Nome));
            bool matriculaIsEmpty = (Matricula != null && !"".Equals(Matricula));

            return nomeIsEmpty && matriculaIsEmpty;
        }
    }
}