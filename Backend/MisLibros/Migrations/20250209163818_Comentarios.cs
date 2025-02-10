using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MisLibros.Migrations
{
    /// <inheritdoc />
    public partial class Comentarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdUsuarioComentario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUsuarioArticulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    TituloApunte = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => new { x.IdUsuarioComentario, x.IdUsuarioArticulo, x.IdLibro, x.TituloApunte })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Comentarios_Articulos_IdUsuarioArticulo_IdLibro_TituloApunte",
                        columns: x => new { x.IdUsuarioArticulo, x.IdLibro, x.TituloApunte },
                        principalTable: "Articulos",
                        principalColumns: new[] { "IdUsuario", "IdLibro", "TituloApunte" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_AspNetUsers_IdUsuarioComentario",
                        column: x => x.IdUsuarioComentario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuarioArticulo_IdLibro_TituloApunte",
                table: "Comentarios",
                columns: new[] { "IdUsuarioArticulo", "IdLibro", "TituloApunte" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");
        }
    }
}
