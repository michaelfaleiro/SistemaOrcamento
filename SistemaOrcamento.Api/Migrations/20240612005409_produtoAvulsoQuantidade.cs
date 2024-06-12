using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaOrcamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class produtoAvulsoQuantidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ProdutosAvulsos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ProdutosAvulsos");
        }
    }
}
