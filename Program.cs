﻿using System;
using System.Globalization;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;


namespace PDF_Table_Extrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /// Exibe os dados extraidos do pdf.
            // Console.WriteLine(ExtarctTable(ExtarctPdf("Sem título 1.pdf")));
            ///Exibe os dados salvos na lista de CompCurri(Componente curricular).
            // PrintTable(ExtarctTable(ExtarctPdf("Nome do documento")));
        }

        

        
        
         public static void PrintTable(string talelaBaguncada)
        {   
            string [] linhasBaguncadas = talelaBaguncada.Split("\n");
            
            CompCurri [] lista = new CompCurri [linhasBaguncadas.Length-1];
            int a =0;
            for(int i = 0 ; i< linhasBaguncadas.Length-1; i++)
            {
                if (linhasBaguncadas[i].Equals("REPROVADO") ||linhasBaguncadas[i].Equals("APROVADO POR"))
                {
                    lista[a++]=  new CompCurri(linhasBaguncadas[i],linhasBaguncadas[i+1],linhasBaguncadas[i+2]);
                    i+=2;
                }else
                {   string temp = linhasBaguncadas[i].Replace(",",".");
                    float ch;
                    bool deu = float.TryParse(temp,out ch);
                    if (deu)
                    {
                      lista[a++]=  new CompCurri(linhasBaguncadas[i],linhasBaguncadas[++i]);  
                    }else
                    {
                       lista[a++]=  new CompCurri(temp);  
                    }
                }
            } 
            foreach (var item in lista)
            {
                if(item == null)
                    break;
                string linha= null, temp = item.ToString();
                for (int i = 0; i < temp.Length; i++)
                {
                    linha +="-";
                }
               Console.WriteLine(linha+"\n"+temp);
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
        }
    }

    public class CompCurri{
        public string AnoLetivo {get;set;}
        public string ComponenteCurricular { get; set; }
        public int QuantAulas { get; set; }
        public float CH { get; set; }
        public string Turma { get; set; }
        public float FrequenciaPorcen { get; set; }
        public float Nota { get; set; }
        public string Status { get; set; }

        public CompCurri(string entrada)
        {
           string [] entradas = entrada.Split(" ");
           AnoLetivo = entradas[0];
           bool vai = true;
           string componenteC= null;
           int posicao =0;
           for (int i = 1; vai; i++)
           {
               int a = 0;
               bool lavai = int.TryParse(entradas[i],out a);
               if(lavai){
                   vai=false;
                    posicao = i;
               }else
               {
                  componenteC += entradas[i]+" ";  
               }
            } 
           ComponenteCurricular = componenteC;
            QuantAulas = int.Parse(entradas[posicao]);
            CH = float.Parse(entradas[++posicao].Replace(",","."));
            Turma = entradas[++posicao];
            FrequenciaPorcen = float.Parse(entradas[++posicao].Replace("--","0"));
            Nota = float.Parse(entradas[++posicao].Replace("---","0"));
            Status = entradas[++posicao];
        }

        public CompCurri(string entradaBugada1,string entradaBugada2,string entradaBugada3)
        {
           string [] entradas = entradaBugada2.Split(" ");
           AnoLetivo = entradas[0];
           bool vai = true;
           string componenteC= null;
           int posicao =0;
           for (int i = 1; vai; i++)
           {
               int a = 0;
               bool lavai = int.TryParse(entradas[i],out a);
               if(lavai){
                   vai=false;
                    posicao = i;
               }else
               {
                  componenteC += entradas[i]+" ";  
               }
           }
           ComponenteCurricular = componenteC;
            QuantAulas = int.Parse(entradas[posicao]);
            CH = float.Parse(entradas[++posicao].Replace(",","."));
            Turma = entradas[++posicao];
            FrequenciaPorcen = float.Parse(entradas[++posicao].Replace("--","0"));
            Nota = float.Parse(entradas[++posicao].Replace("---","0"));
            if(entradaBugada1.Equals("REPROVADO"))
            {
                Status = entradaBugada1+" "+entradas[++posicao]+" "+entradas[++posicao]+" "+entradas[++posicao]+" "+entradaBugada3;
            }else
            {
            Status = entradaBugada1+" "+entradas[++posicao]+" "+entradaBugada3;

            }
        }

         public CompCurri(string entradaBugada1,string entradaBugada2)
        {
           string [] entradas = entradaBugada2.Split(" ");
           AnoLetivo = entradas[0];
           bool vai = true;
           string componenteC= null;
           int posicao =0;
           for (int i = 1; vai; i++)
           {
               int a = 0;
               bool lavai = int.TryParse(entradas[i],out a);
               if(lavai){
                   vai=false;
                    posicao = i;
               }else
               {
                  componenteC += entradas[i]+" ";  
               }
           }
           ComponenteCurricular = componenteC;
            QuantAulas = int.Parse(entradas[posicao]);
            CH = float.Parse(entradaBugada1,CultureInfo.InvariantCulture);
            Turma = entradas[++posicao];
            FrequenciaPorcen = float.Parse(entradas[++posicao].Replace("--","0"));
            Nota = float.Parse(entradas[++posicao].Replace("--","0"));
            Status = entradas[++posicao];
        }

        
        public override string ToString(){
            
            int comprimento = 60-ComponenteCurricular.Length;
            for (int i = 0 ; i < comprimento ; i++)
            {
                ComponenteCurricular+=" ";
            }
            comprimento = 40-Status.Length;
            for (int i = 0 ; i < comprimento ; i++)
            {
                Status+=" ";
            }
            string temp = (FrequenciaPorcen/10).ToString();
            if(FrequenciaPorcen/10<100){

                temp = " "+temp;
                if (FrequenciaPorcen/10<10)
                {
                    temp = " "+temp;
                }
            }
            string temp2 = (Nota/10).ToString("0.0");
            if(Nota/10<10){

                temp2 = " "+temp2;
                
            }
            
            
            return  "| " + AnoLetivo+" | " + ComponenteCurricular +" | " + (QuantAulas>100?QuantAulas:" "+ QuantAulas)+" | " +(CH/100>100?CH/100:" "+CH/100)+" | " +Turma+" | " +temp+"% | " +temp2+" | " +Status+" |";            
        }

    }
         
}
