using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class PromjenaRN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RadniNalog_Predmet_PredmetID",
                table: "RadniNalog");

            migrationBuilder.DropIndex(
                name: "IX_RadniNalog_PredmetID",
                table: "RadniNalog");

            migrationBuilder.DropColumn(
                name: "PredmetID",
                table: "RadniNalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PredmetID",
                table: "RadniNalog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RadniNalog_PredmetID",
                table: "RadniNalog",
                column: "PredmetID");

            migrationBuilder.AddForeignKey(
                name: "FK_RadniNalog_Predmet_PredmetID",
                table: "RadniNalog",
                column: "PredmetID",
                principalTable: "Predmet",
                principalColumn: "PredmetID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
