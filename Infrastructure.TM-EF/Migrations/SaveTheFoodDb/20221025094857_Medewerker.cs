using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    public partial class Medewerker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersoneelsNummer = table.Column<int>(type: "int", nullable: false),
                    locatieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Kantines_locatieId",
                        column: x => x.locatieId,
                        principalTable: "Kantines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_locatieId",
                table: "Medewerkers",
                column: "locatieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medewerkers");
        }
    }
}
