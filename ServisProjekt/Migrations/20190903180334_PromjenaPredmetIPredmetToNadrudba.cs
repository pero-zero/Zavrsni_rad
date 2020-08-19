using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class PromjenaPredmetIPredmetToNadrudba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Količina",
                table: "Predmet");

            migrationBuilder.AddColumn<int>(
                name: "Količina",
                table: "NarudžbaToPredmet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Količina",
                table: "NarudžbaToPredmet");

            migrationBuilder.AddColumn<int>(
                name: "Količina",
                table: "Predmet",
                nullable: false,
                defaultValue: 0);
        }
    }
}
