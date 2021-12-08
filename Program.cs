using System;
using System.Linq;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EntityFramework.Exceptions.Common;
using EntityFramework.Exceptions.SqlServer;

namespace PDF_Table_Extrator
{
    public class Program
    {

        // public IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {

            using var host = CreateHostBuilder(args).Build();

            var programa = host.Services.GetRequiredService<CarregarHistoricoServices>();
            try
            {
                var exemplo = programa.CarregarArquivoExemplo();
                PrintTableAluno(exemplo);
                programa.GravarNoBancoDados(exemplo);
                var notas = programa.ObterNotasAcimaDe(95, exemplo.AlunoId);
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex}");
            }
            host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    var provider = configuration.GetValue("Provider", "SqlServer");

                    services.AddScoped<CarregarHistoricoServices>();
                    services.AddTransient<Program>();



                    //options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"))
                    //options.UseInMemoryDatabase(databaseName: "HistoricoAluno"));


                    services.AddDbContext<HistoricoDBContext>(
                        //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli
                        options => _ = provider switch
                        {
                            "SqlServer" => options.UseSqlServer(
                                configuration.GetConnectionString("DefaultConnection")).UseExceptionProcessor(),
                            _ => options.UseInMemoryDatabase(databaseName: "HistoricoAluno")
                        });

                }).UseConsoleLifetime();
        }

        public Program()
        {

        }



        private static void PrintTableAluno(HistoricoAluno historico)
        {
            Console.WriteLine($"Histórico de {historico.Aluno.Nome} ({historico.Aluno.Matricula})");
            PrintTableDisciplinas(historico.Disciplinas.ToArray());
        }



        private static void ExibirNotasAcimaDe(IEnumerable<DisciplinaAluno> disciplinasDoAluno)
        {
            Console.WriteLine($"A quantidade encontrada foi de {disciplinasDoAluno.Count()}");
            PrintTableDisciplinas(disciplinasDoAluno.ToArray());


        }


        public static void PrintTableDisciplinas(params DisciplinaAluno[] disciplinasAluno)
        {

            foreach (var item in disciplinasAluno)
            {
                if (item == null)
                    break;
                string linha = null, temp = item.ToString();
                for (int i = 0; i < temp.Length; i++)
                {
                    linha += "-";
                }
                Console.WriteLine("-".PadLeft(temp.Length, '-'));
                Console.WriteLine();
                Console.WriteLine(temp);
            }
            Console.WriteLine("-".PadLeft(150, '-'));
        }


    }
}