using Microsoft.EntityFrameworkCore.Migrations;

namespace PDF_Table_Extrator.Migrations
{
    public partial class UpdateNomeTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasDosAlunos_HistoricoDeAlunos_HistoricoAlunoId",
                table: "DisciplinasDosAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoDeAlunos",
                table: "HistoricoDeAlunos");

            migrationBuilder.RenameTable(
                name: "HistoricoDeAlunos",
                newName: "HistoricoAluno");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoAluno",
                table: "HistoricoAluno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasDosAlunos_HistoricoAluno_HistoricoAlunoId",
                table: "DisciplinasDosAlunos",
                column: "HistoricoAlunoId",
                principalTable: "HistoricoAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasDosAlunos_HistoricoAluno_HistoricoAlunoId",
                table: "DisciplinasDosAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoAluno",
                table: "HistoricoAluno");

            migrationBuilder.RenameTable(
                name: "HistoricoAluno",
                newName: "HistoricoDeAlunos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoDeAlunos",
                table: "HistoricoDeAlunos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasDosAlunos_HistoricoDeAlunos_HistoricoAlunoId",
                table: "DisciplinasDosAlunos",
                column: "HistoricoAlunoId",
                principalTable: "HistoricoDeAlunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
