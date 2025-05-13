using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommercePedidos.Migrations
{
    /// <inheritdoc />
    public partial class EcommercePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    produto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    valor = table.Column<float>(type: "real", maxLength: 50, nullable: false),
                    statuspedido = table.Column<int>(type: "integer", nullable: false),
                    tipofrete = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "pedido",
                columns: new[] { "id", "produto", "statuspedido", "tipofrete", "valor" },
                values: new object[,]
                {
                    { 1, "Blusa", 1, 2, 20.5f },
                    { 2, "Calça", 1, 2, 50.5f },
                    { 3, "Sapato", 1, 2, 100f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido");
        }
    }
}
