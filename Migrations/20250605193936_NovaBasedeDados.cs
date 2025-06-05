using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EliminIQ_TCC.Migrations
{
    /// <inheritdoc />
    public partial class NovaBasedeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Criar_Quizz",
                columns: table => new
                {
                    Fk_Usuario = table.Column<int>(type: "integer", nullable: false),
                    Fk_Quizz = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criar_Quizz", x => new { x.Fk_Usuario, x.Fk_Quizz });
                });

            migrationBuilder.CreateTable(
                name: "Dificuldade",
                columns: table => new
                {
                    Id_Dificuldade = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome_Dificuldade = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dificuldade", x => x.Id_Dificuldade);
                });

            migrationBuilder.CreateTable(
                name: "Privacidade",
                columns: table => new
                {
                    Id_Privacidade = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome_Privacidade = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privacidade", x => x.Id_Privacidade);
                });

            migrationBuilder.CreateTable(
                name: "TipoQuizz",
                columns: table => new
                {
                    Id_Tipo_Quiz = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tema_Quiz = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoQuizz", x => x.Id_Tipo_Quiz);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome_Usuario = table.Column<string>(type: "text", nullable: false),
                    Email_Usuario = table.Column<string>(type: "text", nullable: false),
                    Senha_Usuario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id_Usuario);
                });

            migrationBuilder.CreateTable(
                name: "Quizz",
                columns: table => new
                {
                    Id_Quiz = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome_Quiz = table.Column<string>(type: "text", nullable: false),
                    Privacidade_Quiz = table.Column<string>(type: "text", nullable: false),
                    Dificuldade_Quiz = table.Column<string>(type: "text", nullable: false),
                    Fk_Tipo_Quiz = table.Column<int>(type: "integer", nullable: false),
                    Fk_Dificuldade = table.Column<int>(type: "integer", nullable: false),
                    Fk_Privacidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizz", x => x.Id_Quiz);
                    table.ForeignKey(
                        name: "FK_Quizz_Dificuldade_Fk_Dificuldade",
                        column: x => x.Fk_Dificuldade,
                        principalTable: "Dificuldade",
                        principalColumn: "Id_Dificuldade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizz_Privacidade_Fk_Privacidade",
                        column: x => x.Fk_Privacidade,
                        principalTable: "Privacidade",
                        principalColumn: "Id_Privacidade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizz_TipoQuizz_Fk_Tipo_Quiz",
                        column: x => x.Fk_Tipo_Quiz,
                        principalTable: "TipoQuizz",
                        principalColumn: "Id_Tipo_Quiz",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pergunta",
                columns: table => new
                {
                    Id_Pergunta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao_Pergunta = table.Column<string>(type: "text", nullable: false),
                    Fk_Quiz = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pergunta", x => x.Id_Pergunta);
                    table.ForeignKey(
                        name: "FK_Pergunta_Quizz_Fk_Quiz",
                        column: x => x.Fk_Quiz,
                        principalTable: "Quizz",
                        principalColumn: "Id_Quiz",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Quizz",
                columns: table => new
                {
                    Fk_Usuario = table.Column<int>(type: "integer", nullable: false),
                    Fk_Quizz = table.Column<int>(type: "integer", nullable: false),
                    Vida = table.Column<int>(type: "integer", nullable: false),
                    StatusVida = table.Column<int>(type: "integer", nullable: false),
                    Respaw = table.Column<int>(type: "integer", nullable: false),
                    QuizzId_Quiz = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId_Usuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Quizz", x => new { x.Fk_Usuario, x.Fk_Quizz });
                    table.ForeignKey(
                        name: "FK_Usuario_Quizz_Quizz_QuizzId_Quiz",
                        column: x => x.QuizzId_Quiz,
                        principalTable: "Quizz",
                        principalColumn: "Id_Quiz");
                    table.ForeignKey(
                        name: "FK_Usuario_Quizz_Usuario_UsuarioId_Usuario",
                        column: x => x.UsuarioId_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "Id_Usuario");
                });

            migrationBuilder.CreateTable(
                name: "Alternativa",
                columns: table => new
                {
                    Id_Alternativa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao_Alternativa = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Fk_Pergunta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativa", x => x.Id_Alternativa);
                    table.ForeignKey(
                        name: "FK_Alternativa_Pergunta_Fk_Pergunta",
                        column: x => x.Fk_Pergunta,
                        principalTable: "Pergunta",
                        principalColumn: "Id_Pergunta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativa_Fk_Pergunta",
                table: "Alternativa",
                column: "Fk_Pergunta");

            migrationBuilder.CreateIndex(
                name: "IX_Pergunta_Fk_Quiz",
                table: "Pergunta",
                column: "Fk_Quiz");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Fk_Dificuldade",
                table: "Quizz",
                column: "Fk_Dificuldade");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Fk_Privacidade",
                table: "Quizz",
                column: "Fk_Privacidade");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Fk_Tipo_Quiz",
                table: "Quizz",
                column: "Fk_Tipo_Quiz");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Quizz_QuizzId_Quiz",
                table: "Usuario_Quizz",
                column: "QuizzId_Quiz");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Quizz_UsuarioId_Usuario",
                table: "Usuario_Quizz",
                column: "UsuarioId_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alternativa");

            migrationBuilder.DropTable(
                name: "Criar_Quizz");

            migrationBuilder.DropTable(
                name: "Usuario_Quizz");

            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Quizz");

            migrationBuilder.DropTable(
                name: "Dificuldade");

            migrationBuilder.DropTable(
                name: "Privacidade");

            migrationBuilder.DropTable(
                name: "TipoQuizz");
        }
    }
}
