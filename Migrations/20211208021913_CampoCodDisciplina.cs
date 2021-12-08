using Microsoft.EntityFrameworkCore.Migrations;

namespace PDF_Table_Extrator.Migrations
{
    public partial class CampoCodDisciplina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComponenteCurricular",
                table: "DisciplinasDosAlunos",
                newName: "Nome");

            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "HistoricoAluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "DisciplinasDosAlunos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAluno_AlunoId",
                table: "HistoricoAluno",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoAluno_Aluno_AlunoId",
                table: "HistoricoAluno",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoAluno_Aluno_AlunoId",
                table: "HistoricoAluno");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_HistoricoAluno_AlunoId",
                table: "HistoricoAluno");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "HistoricoAluno");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "DisciplinasDosAlunos");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "DisciplinasDosAlunos",
                newName: "ComponenteCurricular");
        }
    }
}
