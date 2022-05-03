using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace testedev.Migrations
{
    public partial class primeira_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NmCategoria = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NmCliente = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NmStatus = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NmTipoMovimentacao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimentacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conteiners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConteinerNumero = table.Column<string>(type: "TEXT", nullable: true),
                    ConteinerTipo = table.Column<byte>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteiners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conteiners_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conteiners_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConteinerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoMovimentacaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Conteiners_ConteinerId",
                        column: x => x.ConteinerId,
                        principalTable: "Conteiners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_TipoMovimentacao_TipoMovimentacaoId",
                        column: x => x.TipoMovimentacaoId,
                        principalTable: "TipoMovimentacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "NmCategoria" },
                values: new object[] { 1, "Importação" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "NmCategoria" },
                values: new object[] { 2, "Exportação" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "NmCliente" },
                values: new object[] { 1, "João Pablo" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "NmCliente" },
                values: new object[] { 2, "Maria Cardoso" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "NmCliente" },
                values: new object[] { 3, "Adalberto Penna" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "NmStatus" },
                values: new object[] { 1, "Vazio" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "NmStatus" },
                values: new object[] { 2, "Cheio" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 1, "Embarque" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 2, "Descarga" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 3, "Gate In" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 4, "Gate Out" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 5, "Reposicionamento" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 6, "Pesagem" });

            migrationBuilder.InsertData(
                table: "TipoMovimentacao",
                columns: new[] { "Id", "NmTipoMovimentacao" },
                values: new object[] { 7, "Scanner" });

            migrationBuilder.CreateIndex(
                name: "IX_Conteiners_CategoriaId",
                table: "Conteiners",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Conteiners_StatusId",
                table: "Conteiners",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ClienteId",
                table: "Movimentacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ConteinerId",
                table: "Movimentacoes",
                column: "ConteinerId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_TipoMovimentacaoId",
                table: "Movimentacoes",
                column: "TipoMovimentacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacoes");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Conteiners");

            migrationBuilder.DropTable(
                name: "TipoMovimentacao");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
