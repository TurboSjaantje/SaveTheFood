using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    public partial class test123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Producten",
                table: "Pakketten");

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

            migrationBuilder.AddColumn<string>(
                name: "Producten",
                table: "Pakketten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
