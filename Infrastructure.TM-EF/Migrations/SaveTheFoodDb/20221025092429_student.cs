using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    public partial class student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studenten",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeboorteDatum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNummer = table.Column<int>(type: "int", nullable: false),
                    StudieStad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoonNummer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenten", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studenten");
        }
    }
}
