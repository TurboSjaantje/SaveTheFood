using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    public partial class addedpakketten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PakketId",
                table: "Producten",
                type: "int",
                nullable: true);

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
                    TypeMaaltijd = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Producten_PakketId",
                table: "Producten",
                column: "PakketId");

            migrationBuilder.CreateIndex(
                name: "IX_Pakketten_GereserveerdDoorEmail",
                table: "Pakketten",
                column: "GereserveerdDoorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Pakketten_kantineId",
                table: "Pakketten",
                column: "kantineId");

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

            migrationBuilder.DropTable(
                name: "Pakketten");

            migrationBuilder.DropIndex(
                name: "IX_Producten_PakketId",
                table: "Producten");

            migrationBuilder.DropColumn(
                name: "PakketId",
                table: "Producten");
        }
    }
}
