﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PDF_Table_Extrator;

namespace PDF_Table_Extrator.Migrations
{
    [DbContext(typeof(HistoricoDBContext))]
    [Migration("20211122234255_UpdateNomeTabela")]
    partial class UpdateNomeTabela
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PDF_Table_Extrator.DisciplinaAluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnoLetivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("CH")
                        .HasColumnType("real");

                    b.Property<string>("ComponenteCurricular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Frequencia")
                        .HasColumnType("real");

                    b.Property<int>("HistoricoAlunoId")
                        .HasColumnType("int");

                    b.Property<float>("Nota")
                        .HasColumnType("real");

                    b.Property<int>("QuantidadeAulas")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turma")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HistoricoAlunoId");

                    b.ToTable("DisciplinasDosAlunos");
                });

            modelBuilder.Entity("PDF_Table_Extrator.HistoricoAluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataArquivo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataLeitura")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HistoricoAluno");
                });

            modelBuilder.Entity("PDF_Table_Extrator.DisciplinaAluno", b =>
                {
                    b.HasOne("PDF_Table_Extrator.HistoricoAluno", "HistoricoAluno")
                        .WithMany("Disciplinas")
                        .HasForeignKey("HistoricoAlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoricoAluno");
                });

            modelBuilder.Entity("PDF_Table_Extrator.HistoricoAluno", b =>
                {
                    b.Navigation("Disciplinas");
                });
#pragma warning restore 612, 618
        }
    }
}
