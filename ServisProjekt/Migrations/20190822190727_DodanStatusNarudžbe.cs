using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class DodanStatusNarudžbe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusNarudžbeID",
                table: "Narudžba",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusNarudžbe",
                columns: table => new
                {
                    StatusNarudžbeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OpisStatusa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNarudžbe", x => x.StatusNarudžbeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Narudžba_StatusNarudžbeID",
                table: "Narudžba",
                column: "StatusNarudžbeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudžba_StatusNarudžbe_StatusNarudžbeID",
                table: "Narudžba",
                column: "StatusNarudžbeID",
                principalTable: "StatusNarudžbe",
                principalColumn: "StatusNarudžbeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudžba_StatusNarudžbe_StatusNarudžbeID",
                table: "Narudžba");

            migrationBuilder.DropTable(
                name: "StatusNarudžbe");

            migrationBuilder.DropIndex(
                name: "IX_Narudžba_StatusNarudžbeID",
                table: "Narudžba");

            migrationBuilder.DropColumn(
                name: "StatusNarudžbeID",
                table: "Narudžba");
        }
    }
}
