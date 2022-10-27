using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PakketProduct");

            migrationBuilder.AddColumn<int>(
                name: "PakketId",
                table: "Producten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producten_PakketId",
                table: "Producten",
                column: "PakketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producten_Pakketten_PakketId",
                table: "Producten",
                column: "PakketId",
                principalTable: "Pakketten",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producten_Pakketten_PakketId",
                table: "Producten");

            migrationBuilder.DropIndex(
                name: "IX_Producten_PakketId",
                table: "Producten");

            migrationBuilder.DropColumn(
                name: "PakketId",
                table: "Producten");

            migrationBuilder.CreateTable(
                name: "PakketProduct",
                columns: table => new
                {
                    ProductenId = table.Column<int>(type: "int", nullable: false),
                    pakkettenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PakketProduct", x => new { x.ProductenId, x.pakkettenId });
                    table.ForeignKey(
                        name: "FK_PakketProduct_Pakketten_pakkettenId",
                        column: x => x.pakkettenId,
                        principalTable: "Pakketten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PakketProduct_Producten_ProductenId",
                        column: x => x.ProductenId,
                        principalTable: "Producten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PakketProduct_pakkettenId",
                table: "PakketProduct",
                column: "pakkettenId");
        }
    }
}
