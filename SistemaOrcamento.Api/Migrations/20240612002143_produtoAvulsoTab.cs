using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaOrcamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class produtoAvulsoTab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoAvulso_Orcamentos_OrcamentoId",
                table: "ProdutoAvulso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoAvulso",
                table: "ProdutoAvulso");

            migrationBuilder.RenameTable(
                name: "ProdutoAvulso",
                newName: "ProdutosAvulsos");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoAvulso_OrcamentoId",
                table: "ProdutosAvulsos",
                newName: "IX_ProdutosAvulsos_OrcamentoId");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "ProdutosAvulsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fabricante",
                table: "ProdutosAvulsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosAvulsos",
                table: "ProdutosAvulsos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosAvulsos_Orcamentos_OrcamentoId",
                table: "ProdutosAvulsos",
                column: "OrcamentoId",
                principalTable: "Orcamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosAvulsos_Orcamentos_OrcamentoId",
                table: "ProdutosAvulsos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosAvulsos",
                table: "ProdutosAvulsos");

            migrationBuilder.DropColumn(
                name: "Fabricante",
                table: "ProdutosAvulsos");

            migrationBuilder.RenameTable(
                name: "ProdutosAvulsos",
                newName: "ProdutoAvulso");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosAvulsos_OrcamentoId",
                table: "ProdutoAvulso",
                newName: "IX_ProdutoAvulso_OrcamentoId");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "ProdutoAvulso",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoAvulso",
                table: "ProdutoAvulso",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoAvulso_Orcamentos_OrcamentoId",
                table: "ProdutoAvulso",
                column: "OrcamentoId",
                principalTable: "Orcamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
