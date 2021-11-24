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
        public Disciplina(string a){

        }

        public Disciplina(string a, string b , string c ){

        }
        public Disciplina(float d,string a, string b , string c ){

        }
        public Disciplina(float d,string a){

        }
    }
}