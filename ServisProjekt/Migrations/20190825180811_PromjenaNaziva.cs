using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class PromjenaNaziva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Poduzeće_ZaposlenikID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ZaposlenikID",
                table: "AspNetUsers",
                newName: "PoduzećeID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ZaposlenikID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PoduzećeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Poduzeće_PoduzećeID",
                table: "AspNetUsers",
                column: "PoduzećeID",
                principalTable: "Poduzeće",
                principalColumn: "PoduzećeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Poduzeće_PoduzećeID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PoduzećeID",
                table: "AspNetUsers",
                newName: "ZaposlenikID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PoduzećeID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ZaposlenikID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Poduzeće_ZaposlenikID",
                table: "AspNetUsers",
                column: "ZaposlenikID",
                principalTable: "Poduzeće",
                principalColumn: "PoduzećeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
