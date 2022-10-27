using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    public partial class thrd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_Kantines_locatieId",
                table: "Medewerkers");

            migrationBuilder.RenameColumn(
                name: "locatieId",
                table: "Medewerkers",
                newName: "LocatieId");

            migrationBuilder.RenameIndex(
                name: "IX_Medewerkers_locatieId",
                table: "Medewerkers",
                newName: "IX_Medewerkers_LocatieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerkers_Kantines_LocatieId",
                table: "Medewerkers",
                column: "LocatieId",
                principalTable: "Kantines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_Kantines_LocatieId",
                table: "Medewerkers");

            migrationBuilder.RenameColumn(
                name: "LocatieId",
                table: "Medewerkers",
                newName: "locatieId");

            migrationBuilder.RenameIndex(
                name: "IX_Medewerkers_LocatieId",
                table: "Medewerkers",
                newName: "IX_Medewerkers_locatieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerkers_Kantines_locatieId",
                table: "Medewerkers",
                column: "locatieId",
                principalTable: "Kantines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
