using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MisLibros.Migrations
{
    /// <inheritdoc />
    public partial class likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    IdUsuarioComentario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUsuarioArticulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    TituloApunte = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.IdUsuarioComentario, x.IdUsuarioArticulo, x.IdLibro, x.TituloApunte })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Likes_Articulos_IdUsuarioArticulo_IdLibro_TituloApunte",
                        columns: x => new { x.IdUsuarioArticulo, x.IdLibro, x.TituloApunte },
                        principalTable: "Articulos",
                        principalColumns: new[] { "IdUsuario", "IdLibro", "TituloApunte" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_IdUsuarioComentario",
                        column: x => x.IdUsuarioComentario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_IdUsuarioArticulo_IdLibro_TituloApunte",
                table: "Likes",
                columns: new[] { "IdUsuarioArticulo", "IdLibro", "TituloApunte" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
