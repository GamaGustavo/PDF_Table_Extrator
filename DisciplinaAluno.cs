using System.Globalization;


namespace PDF_Table_Extrator
{
    public class DisciplinaAluno
    {
        public string AnoLetivo { get; protected set; }
        public string ComponenteCurricular { get; protected set; }
        public int QuantidadeAulas { get; protected set; }
        public float CH { get; protected set; }
        public string Turma { get; protected set; }
        public float Frequencia { get; protected set; }
        public float Nota { get; protected set; }
        public string Status { get; protected set; }
        public DisciplinaAluno() { }
        public static DisciplinaAluno Parse(string entrada)
        {
            string[] entradas = entrada.Split(" ");
            bool vai = true;
            string componenteC = null;
            int posicao = 0;
            for (int i = 1; vai; i++)
            {
                int a = 0;
                bool lavai = int.TryParse(entradas[i], out a);
                if (lavai)
                {
                    vai = false;
                    posicao = i;
                }
                else
                {
                    componenteC += entradas[i] + " ";
                }
            }
            return new DisciplinaAluno()
            {
                AnoLetivo = entradas[0],
                ComponenteCurricular = componenteC,
                QuantidadeAulas = int.Parse(entradas[posicao]),
                CH = float.Parse(entradas[++posicao].Replace(",", ".")),
                Turma = entradas[++posicao],
                Frequencia = float.Parse(entradas[++posicao].Replace("--", "0")),
                Nota = float.Parse(entradas[++posicao].Replace("---", "0")),
                Status = entradas[++posicao]
            };
        }

        public static DisciplinaAluno Parse(string entradaBugada1, string entradaBugada2, string entradaBugada3)
        {
            string[] entradas = entradaBugada2.Split(" ");

            bool vai = true;
            string componenteC = null;
            int posicao = 0;
            for (int i = 1; vai; i++)
            {
                int a = 0;
                bool lavai = int.TryParse(entradas[i], out a);
                if (lavai)
                {
                    vai = false;
                    posicao = i;
                }
                else
                {
                    componenteC += entradas[i] + " ";
                }
            }
            var disciplina = new DisciplinaAluno()
            {
                AnoLetivo = entradas[0],
                ComponenteCurricular = componenteC,
                QuantidadeAulas = int.Parse(entradas[posicao]),
                CH = float.Parse(entradas[++posicao].Replace(",", ".")),
                Turma = entradas[++posicao],
                Frequencia = float.Parse(entradas[++posicao].Replace("--", "0")),
                Nota = float.Parse(entradas[++posicao].Replace("---", "0"))
            };

            if (entradaBugada1.Equals("REPROVADO"))
            {
                disciplina.Status = entradaBugada1 + " " + entradas[++posicao] + " " + entradas[++posicao] + " " + entradas[++posicao] + " " + entradaBugada3;
            }
            else
            {
                disciplina.Status = entradaBugada1 + " " + entradas[++posicao] + " " + entradaBugada3;

            }
            return disciplina;
        }

        public static DisciplinaAluno Parse(string entradaBugada1, string entradaBugada2)
        {
            string[] entradas = entradaBugada2.Split(" ");
            bool vai = true;
            string componenteC = null;
            int posicao = 0;
            for (int i = 1; vai; i++)
            {
                int a = 0;
                bool lavai = int.TryParse(entradas[i], out a);
                if (lavai)
                {
                    vai = false;
                    posicao = i;
                }
                else
                {
                    componenteC += entradas[i] + " ";
                }
            }
            return new DisciplinaAluno
            {
                AnoLetivo = entradas[0],
                ComponenteCurricular = componenteC,
                QuantidadeAulas = int.Parse(entradas[posicao]),
                CH = float.Parse(entradaBugada1, CultureInfo.InvariantCulture),
                Turma = entradas[++posicao],
                Frequencia = float.Parse(entradas[++posicao].Replace("--", "0")),
                Nota = float.Parse(entradas[++posicao].Replace("--", "0")),
                Status = entradas[++posicao]
            };

        }


        public override string ToString()
        {

            int comprimento = 60 - ComponenteCurricular.Length;
            ComponenteCurricular = ComponenteCurricular.PadRight(60, ' ');
            Status = Status.PadRight(60, ' ');


            string temp = (Frequencia / 10).ToString();
            if (Frequencia / 10 < 100)
            {

                temp = " " + temp;
                if (Frequencia / 10 < 10)
                {
                    temp = " " + temp;
                }
            }
            string temp2 = (Nota / 10).ToString("0.0");
            if (Nota / 10 < 10)
            {

                temp2 = " " + temp2;

            }
            return string.Join(" | ",
                " | ",
                AnoLetivo,
                ComponenteCurricular,
                (QuantidadeAulas > 100 ? QuantidadeAulas : " " + QuantidadeAulas),
                (CH / 100 > 100 ? CH / 100 : " " + CH / 100),
                Turma,
                temp + "%",
                temp2,
                Status,
                "|"
            );
        }

    }

}
