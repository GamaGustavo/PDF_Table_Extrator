using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace PDF_Table_Extrator
{
    public class DisciplinaAluno
    {
        public int Id { get; set; }
        //[MaxLength(10), StringLength(10)]
        public string AnoLetivo { get; protected set; }
        public string Codigo { get; protected set; }
        public string Nome { get; protected set; }
        
        public int QuantidadeAulas { get; protected set; }
        public float CH { get; protected set; }
        public string Turma { get; protected set; }
        public float Frequencia { get; protected set; }
        public float Nota { get; set; }
        public string Status { get; protected set; }

        public int HistoricoAlunoId { get; set; }
        public HistoricoAluno HistoricoAluno { get; set; }

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
            var componente = componenteC.Split(" ", 2, System.StringSplitOptions.TrimEntries);
            var d = new DisciplinaAluno()
            {
                AnoLetivo = entradas[0].Trim(),
                Codigo = componente[0],
                Nome = componente[1],
                QuantidadeAulas = int.Parse(entradas[posicao]),
                CH = float.Parse(entradas[++posicao].Replace(",", ".")),
                Turma = entradas[++posicao].Trim(),
                Frequencia = float.Parse(entradas[++posicao].Replace("--", "0")),
                Nota = float.Parse(entradas[++posicao].Replace("---", "0")),
                Status = entradas[++posicao].Trim()
            };
            return d;
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
            var componente = componenteC.Split(" ", 2, System.StringSplitOptions.TrimEntries);

            var disciplina = new DisciplinaAluno()
            {
                AnoLetivo = entradas[0].Trim(),
                Codigo = componente[0],
                Nome = componente[1],
                QuantidadeAulas = int.Parse(entradas[posicao]),
                CH = float.Parse(entradas[++posicao].Replace(",", ".")),
                Turma = entradas[++posicao].Trim(),
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
            disciplina.Status = disciplina.Status.Trim();
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
            var componente = componenteC.Split(" ", 2, System.StringSplitOptions.TrimEntries);

            return new DisciplinaAluno
            {
                AnoLetivo = entradas[0].Trim(),
                Codigo = componente[0],
                Nome = componente[1],
                QuantidadeAulas = int.Parse(entradas[posicao]),
                CH = float.Parse(entradaBugada1, CultureInfo.InvariantCulture),
                Turma = entradas[++posicao].Trim(),
                Frequencia = float.Parse(entradas[++posicao].Replace("--", "0")),
                Nota = float.Parse(entradas[++posicao].Replace("--", "0")),
                Status = entradas[++posicao].Trim()
            };

        }


        public override string ToString()
        {

            var componenteCurricular = Nome.PadRight(60, ' ');
            var status = Status.PadRight(60, ' ');


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
                componenteCurricular,
                (QuantidadeAulas > 100 ? QuantidadeAulas : " " + QuantidadeAulas),
                (CH / 100 > 100 ? CH / 100 : " " + CH / 100),
                Turma,
                temp + "%",
                temp2,
                status,
                "|"
            );
        }

    }

}
