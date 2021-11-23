using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDF_Table_Extrator.Migrations
{
    public partial class TabelasIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricoDeAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataLeitura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataArquivo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoDeAlunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinasDosAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnoLetivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponenteCurricular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeAulas = table.Column<int>(type: "int", nullable: false),
                    CH = table.Column<float>(type: "real", nullable: false),
                    Turma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequencia = table.Column<float>(type: "real", nullable: false),
                    Nota = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoAlunoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinasDosAlunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinasDosAlunos_HistoricoDeAlunos_HistoricoAlunoId",
                        column: x => x.HistoricoAlunoId,
                        principalTable: "HistoricoDeAlunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasDosAlunos_HistoricoAlunoId",
                table: "DisciplinasDosAlunos",
                column: "HistoricoAlunoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplinasDosAlunos");

            migrationBuilder.DropTable(
                name: "HistoricoDeAlunos");
        }
    }
}
