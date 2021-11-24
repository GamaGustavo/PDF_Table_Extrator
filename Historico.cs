using System;
using System.Collections;

namespace historico{
    public class Historico{
        public ArrayList Disciplinas { get; set; }
        public Aluno   Aluno{get ; set;}
        public Historico(){
            this.Disciplinas  = new ArrayList();
            this.Aluno = new Aluno();
        }
        public Historico(ArrayList disciplinas, Aluno aluno){
            this.Disciplinas  = disciplinas;
            this.Aluno = aluno;
        }
    }
}