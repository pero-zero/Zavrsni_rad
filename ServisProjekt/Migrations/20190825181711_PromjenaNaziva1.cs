using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class PromjenaNaziva1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KodGreške",
                table: "Narudžba",
                newName: "ŠifraGreške");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ŠifraGreške",
                table: "Narudžba",
                newName: "KodGreške");
        }
    }
}
