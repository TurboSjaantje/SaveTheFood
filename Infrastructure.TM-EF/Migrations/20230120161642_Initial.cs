using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kantines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarmeMaaltijden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kantines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlcoholHoudend = table.Column<bool>(type: "bit", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producten", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersoneelsNummer = table.Column<int>(type: "int", nullable: false),
                    LocatieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Kantines_LocatieId",
                        column: x => x.LocatieId,
                        principalTable: "Kantines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pakketten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kantineId = table.Column<int>(type: "int", nullable: false),
                    DatumTijd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OphaalTijd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AchtienPlus = table.Column<bool>(type: "bit", nullable: false),
                    Prijs = table.Column<double>(type: "float", nullable: false),
                    TypeMaaltijd = table.Column<int>(type: "int", nullable: false),
                    GereserveerdDoorEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pakketten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pakketten_Kantines_kantineId",
                        column: x => x.kantineId,
                        principalTable: "Kantines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pakketten_Studenten_GereserveerdDoorEmail",
                        column: x => x.GereserveerdDoorEmail,
                        principalTable: "Studenten",
                        principalColumn: "Email");
                });

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
                name: "IX_Medewerkers_LocatieId",
                table: "Medewerkers",
                column: "LocatieId");

            migrationBuilder.CreateIndex(
                name: "IX_PakketProduct_pakkettenId",
                table: "PakketProduct",
                column: "pakkettenId");

            migrationBuilder.CreateIndex(
                name: "IX_Pakketten_GereserveerdDoorEmail",
                table: "Pakketten",
                column: "GereserveerdDoorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Pakketten_kantineId",
                table: "Pakketten",
                column: "kantineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medewerkers");

            migrationBuilder.DropTable(
                name: "PakketProduct");

            migrationBuilder.DropTable(
                name: "Pakketten");

            migrationBuilder.DropTable(
                name: "Producten");

            migrationBuilder.DropTable(
                name: "Kantines");

            migrationBuilder.DropTable(
                name: "Studenten");
        }
    }
}
