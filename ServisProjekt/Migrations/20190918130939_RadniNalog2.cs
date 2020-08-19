using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class RadniNalog2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KorisnikID",
                table: "RadniNalog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RadniNalog_KorisnikID",
                table: "RadniNalog",
                column: "KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_RadniNalog_AspNetUsers_KorisnikID",
                table: "RadniNalog",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RadniNalog_AspNetUsers_KorisnikID",
                table: "RadniNalog");

            migrationBuilder.DropIndex(
                name: "IX_RadniNalog_KorisnikID",
                table: "RadniNalog");

            migrationBuilder.DropColumn(
                name: "KorisnikID",
                table: "RadniNalog");
        }
    }
}
