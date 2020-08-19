using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServisProjekt.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObiteljPrintera",
                columns: table => new
                {
                    ObiteljPrinteraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Obitelj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObiteljPrintera", x => x.ObiteljPrinteraID);
                });

            migrationBuilder.CreateTable(
                name: "Poduzeće",
                columns: table => new
                {
                    PoduzećeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false),
                    Adresa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poduzeće", x => x.PoduzećeID);
                });

            migrationBuilder.CreateTable(
                name: "Skladište",
                columns: table => new
                {
                    SkladišteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivSkladišta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skladište", x => x.SkladišteID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaNarudžbe",
                columns: table => new
                {
                    VrstaNarudžbeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaNarudžbe", x => x.VrstaNarudžbeID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaPredmeta",
                columns: table => new
                {
                    VrstaPredmetaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vrsta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaPredmeta", x => x.VrstaPredmetaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    ZaposlenikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Poduzeće_ZaposlenikID",
                        column: x => x.ZaposlenikID,
                        principalTable: "Poduzeće",
                        principalColumn: "PoduzećeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    PrinterID = table.Column<string>(nullable: false),
                    NazivModela = table.Column<string>(nullable: true),
                    LokacijaID = table.Column<int>(nullable: false),
                    VrstaPrinteraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.PrinterID);
                    table.ForeignKey(
                        name: "FK_Printer_Poduzeće_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Poduzeće",
                        principalColumn: "PoduzećeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Printer_ObiteljPrintera_VrstaPrinteraID",
                        column: x => x.VrstaPrinteraID,
                        principalTable: "ObiteljPrintera",
                        principalColumn: "ObiteljPrinteraID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Predmet",
                columns: table => new
                {
                    PredmetID = table.Column<string>(nullable: false),
                    Naziv = table.Column<string>(nullable: false),
                    Cijena = table.Column<double>(nullable: false),
                    Dobavljač = table.Column<string>(nullable: true),
                    Količina = table.Column<int>(nullable: false),
                    MinimalnaKoličina = table.Column<int>(nullable: false),
                    NaSkladistu = table.Column<int>(nullable: false),
                    SkladišteID = table.Column<int>(nullable: false),
                    PrinterID = table.Column<int>(nullable: false),
                    VrstaPredmetaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.PredmetID);
                    table.ForeignKey(
                        name: "FK_Predmet_ObiteljPrintera_PrinterID",
                        column: x => x.PrinterID,
                        principalTable: "ObiteljPrintera",
                        principalColumn: "ObiteljPrinteraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Predmet_Skladište_SkladišteID",
                        column: x => x.SkladišteID,
                        principalTable: "Skladište",
                        principalColumn: "SkladišteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Predmet_VrstaPredmeta_VrstaPredmetaID",
                        column: x => x.VrstaPredmetaID,
                        principalTable: "VrstaPredmeta",
                        principalColumn: "VrstaPredmetaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Narudžba",
                columns: table => new
                {
                    NarudžbaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemeNaruđbe = table.Column<DateTime>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    Counteri = table.Column<int>(nullable: false),
                    KodGreške = table.Column<string>(nullable: true),
                    PoduzećeID = table.Column<int>(nullable: false),
                    NaručiteljID = table.Column<string>(nullable: true),
                    PrinterID = table.Column<string>(nullable: true),
                    VrstaNarudžbeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudžba", x => x.NarudžbaID);
                    table.ForeignKey(
                        name: "FK_Narudžba_AspNetUsers_NaručiteljID",
                        column: x => x.NaručiteljID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Narudžba_Poduzeće_PoduzećeID",
                        column: x => x.PoduzećeID,
                        principalTable: "Poduzeće",
                        principalColumn: "PoduzećeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudžba_Printer_PrinterID",
                        column: x => x.PrinterID,
                        principalTable: "Printer",
                        principalColumn: "PrinterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Narudžba_VrstaNarudžbe_VrstaNarudžbeID",
                        column: x => x.VrstaNarudžbeID,
                        principalTable: "VrstaNarudžbe",
                        principalColumn: "VrstaNarudžbeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NarudžbaToPredmet",
                columns: table => new
                {
                    NarudžbaToPredmetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PredmetiID = table.Column<string>(nullable: true),
                    NarudžbeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudžbaToPredmet", x => x.NarudžbaToPredmetID);
                    table.ForeignKey(
                        name: "FK_NarudžbaToPredmet_Narudžba_NarudžbeID",
                        column: x => x.NarudžbeID,
                        principalTable: "Narudžba",
                        principalColumn: "NarudžbaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NarudžbaToPredmet_Predmet_PredmetiID",
                        column: x => x.PredmetiID,
                        principalTable: "Predmet",
                        principalColumn: "PredmetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ZaposlenikID",
                table: "AspNetUsers",
                column: "ZaposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudžba_NaručiteljID",
                table: "Narudžba",
                column: "NaručiteljID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudžba_PoduzećeID",
                table: "Narudžba",
                column: "PoduzećeID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudžba_PrinterID",
                table: "Narudžba",
                column: "PrinterID");

            migrationBuilder.CreateIndex(
                name: "IX_Narudžba_VrstaNarudžbeID",
                table: "Narudžba",
                column: "VrstaNarudžbeID");

            migrationBuilder.CreateIndex(
                name: "IX_NarudžbaToPredmet_NarudžbeID",
                table: "NarudžbaToPredmet",
                column: "NarudžbeID");

            migrationBuilder.CreateIndex(
                name: "IX_NarudžbaToPredmet_PredmetiID",
                table: "NarudžbaToPredmet",
                column: "PredmetiID");

            migrationBuilder.CreateIndex(
                name: "IX_Predmet_PrinterID",
                table: "Predmet",
                column: "PrinterID");

            migrationBuilder.CreateIndex(
                name: "IX_Predmet_SkladišteID",
                table: "Predmet",
                column: "SkladišteID");

            migrationBuilder.CreateIndex(
                name: "IX_Predmet_VrstaPredmetaID",
                table: "Predmet",
                column: "VrstaPredmetaID");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_LokacijaID",
                table: "Printer",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_VrstaPrinteraID",
                table: "Printer",
                column: "VrstaPrinteraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "NarudžbaToPredmet");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Narudžba");

            migrationBuilder.DropTable(
                name: "Predmet");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.DropTable(
                name: "VrstaNarudžbe");

            migrationBuilder.DropTable(
                name: "Skladište");

            migrationBuilder.DropTable(
                name: "VrstaPredmeta");

            migrationBuilder.DropTable(
                name: "Poduzeće");

            migrationBuilder.DropTable(
                name: "ObiteljPrintera");
        }
    }
}
