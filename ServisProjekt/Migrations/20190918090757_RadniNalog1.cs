using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class RadniNalog1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RadniNalog",
                columns: table => new
                {
                    RadniNalogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true),
                    VrijemeDolaska = table.Column<DateTime>(nullable: false),
                    VrijemeOdlaska = table.Column<DateTime>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Counter = table.Column<int>(nullable: false),
                    NarudžbaID = table.Column<int>(nullable: false),
                    PredmetID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadniNalog", x => x.RadniNalogID);
                    table.ForeignKey(
                        name: "FK_RadniNalog_Narudžba_NarudžbaID",
                        column: x => x.NarudžbaID,
                        principalTable: "Narudžba",
                        principalColumn: "NarudžbaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RadniNalog_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "PredmetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OtpisSaSkladišta",
                columns: table => new
                {
                    OtpisSaSkladištaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RadniNalogID = table.Column<int>(nullable: false),
                    PredmetID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpisSaSkladišta", x => x.OtpisSaSkladištaID);
                    table.ForeignKey(
                        name: "FK_OtpisSaSkladišta_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "PredmetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OtpisSaSkladišta_RadniNalog_RadniNalogID",
                        column: x => x.RadniNalogID,
                        principalTable: "RadniNalog",
                        principalColumn: "RadniNalogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtpisSaSkladišta_PredmetID",
                table: "OtpisSaSkladišta",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_OtpisSaSkladišta_RadniNalogID",
                table: "OtpisSaSkladišta",
                column: "RadniNalogID");

            migrationBuilder.CreateIndex(
                name: "IX_RadniNalog_NarudžbaID",
                table: "RadniNalog",
                column: "NarudžbaID");

            migrationBuilder.CreateIndex(
                name: "IX_RadniNalog_PredmetID",
                table: "RadniNalog",
                column: "PredmetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpisSaSkladišta");

            migrationBuilder.DropTable(
                name: "RadniNalog");
        }
    }
}
