using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class PopulateTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Estudar DOTNET', 'Aplicar conhecimentos criando uma API', 'D', NOW(), NOW(), 1)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Ir treinar', 'Academia', 'C', NOW(), NOW(), 1)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Jogar free fire', 'Jogar com os amigos', 'D', NOW(), NOW(), 2)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Ler livro', 'Inteligencia Emocional', 'C', NOW(), NOW(), 3)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Estudar Direito Constitucional', 'VADE MECUM', 'D', NOW(), NOW(), 4)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Fazer cardio na esteira', '15 minutos', 'C', NOW(), NOW(), 5)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Estudar Power BI', 'Teste da empresa', 'C', NOW(), NOW(), 6)");
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Status, CreatedAt, UpdatedAt, UserId) VALUES ('Jogar Fórmula 1', 'no xbox', 'D', NOW(), NOW(), 7)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Tasks");
        }
    }
}
