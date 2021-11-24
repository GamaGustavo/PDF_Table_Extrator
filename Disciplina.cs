using System;
using System.Globalization;

namespace historico{
    class Disciplina{
        public string AnoLetivo {get;set;}
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int QuantAulas { get; set; }
        public float CH { get; set; }
        public string Turma { get; set; }
        public float FrequenciaPorcen { get; set; }
        public float Nota { get; set; }
        public string Status { get; set; }

        
        public Disciplina(){
        }
        public Disciplina(string linha){
            string [] vetorDeLinhas = linha.Split(' ');
            int countVectorLinhas=vetorDeLinhas.Length;
            AnoLetivo = vetorDeLinhas[0];
            Codigo = vetorDeLinhas[1];
            string nome = null;
            for (int i = 2; i < vetorDeLinhas.Length-6; i++)
            {
                nome += vetorDeLinhas[i];
            }
            Nome = nome;
            QuantAulas = int.Parse(vetorDeLinhas[countVectorLinhas-6]);
            CH = "--".Equals(vetorDeLinhas[countVectorLinhas-5])?0 : float.Parse(vetorDeLinhas[countVectorLinhas-5]);
            Turma = vetorDeLinhas[countVectorLinhas-4];
            FrequenciaPorcen = "--".Equals(vetorDeLinhas[countVectorLinhas-3])?0 : float.Parse(vetorDeLinhas[countVectorLinhas-3],new CultureInfo("en-US"));
            Nota = "---".Equals(vetorDeLinhas[countVectorLinhas-2])?0 : float.Parse(vetorDeLinhas[countVectorLinhas-2],new CultureInfo("en-US"));
            Status = vetorDeLinhas[countVectorLinhas-1];            
        }

        public Disciplina(string linha, string b , string c ){

        }
        public Disciplina(float d,string linha, string b , string c ){

        }
        public Disciplina(float d,string linha){

        }
        public override string ToString()
        {
            return "AnoLetivo : "+AnoLetivo+"\n"+
            "Codigo : "+Codigo+"\n"+
            "Nome : "+Nome+"\n"+
            "QuantAulas : "+QuantAulas+"\n"+
            "CH : "+CH+"\n"+
            "Turma : "+Turma+"\n"+
            "FrequenciaPorcen : "+FrequenciaPorcen+"\n"+
            "Nota : "+Nota+"\n"+
            "Status : "+Status+"\n";

        }
    }
}