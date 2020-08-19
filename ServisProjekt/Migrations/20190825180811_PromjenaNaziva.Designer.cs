﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServisProjekt.Data;

namespace ServisProjekt.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190825180811_PromjenaNaziva")]
    partial class PromjenaNaziva
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ServisProjekt.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PoduzećeID");

                    b.Property<bool>("PrviLogin");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PoduzećeID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ServisProjekt.Models.Narudžba", b =>
                {
                    b.Property<int>("NarudžbaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Counteri");

                    b.Property<string>("KodGreške");

                    b.Property<string>("NaručiteljID");

                    b.Property<string>("Opis");

                    b.Property<int>("PoduzećeID");

                    b.Property<string>("PrinterID");

                    b.Property<int>("StatusNarudžbeID");

                    b.Property<DateTime>("VrijemeNaruđbe");

                    b.Property<int>("VrstaNarudžbeID");

                    b.HasKey("NarudžbaID");

                    b.HasIndex("NaručiteljID");

                    b.HasIndex("PoduzećeID");

                    b.HasIndex("PrinterID");

                    b.HasIndex("StatusNarudžbeID");

                    b.HasIndex("VrstaNarudžbeID");

                    b.ToTable("Narudžba");
                });

            modelBuilder.Entity("ServisProjekt.Models.NarudžbaToPredmet", b =>
                {
                    b.Property<int>("NarudžbaToPredmetID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NarudžbeID");

                    b.Property<string>("PredmetiID");

                    b.HasKey("NarudžbaToPredmetID");

                    b.HasIndex("NarudžbeID");

                    b.HasIndex("PredmetiID");

                    b.ToTable("NarudžbaToPredmet");
                });

            modelBuilder.Entity("ServisProjekt.Models.ObiteljPrintera", b =>
                {
                    b.Property<int>("ObiteljPrinteraID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Obitelj");

                    b.HasKey("ObiteljPrinteraID");

                    b.ToTable("ObiteljPrintera");
                });

            modelBuilder.Entity("ServisProjekt.Models.Poduzeće", b =>
                {
                    b.Property<int>("PoduzećeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired();

                    b.Property<string>("Grad")
                        .IsRequired();

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("PoduzećeID");

                    b.ToTable("Poduzeće");
                });

            modelBuilder.Entity("ServisProjekt.Models.Predmet", b =>
                {
                    b.Property<string>("PredmetID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cijena");

                    b.Property<string>("Dobavljač");

                    b.Property<int>("Količina");

                    b.Property<int>("MinimalnaKoličina");

                    b.Property<int>("NaSkladistu");

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.Property<int>("PrinterID");

                    b.Property<int>("SkladišteID");

                    b.Property<int>("VrstaPredmetaID");

                    b.HasKey("PredmetID");

                    b.HasIndex("PrinterID");

                    b.HasIndex("SkladišteID");

                    b.HasIndex("VrstaPredmetaID");

                    b.ToTable("Predmet");
                });

            modelBuilder.Entity("ServisProjekt.Models.Printer", b =>
                {
                    b.Property<string>("PrinterID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LokacijaID");

                    b.Property<string>("NazivModela");

                    b.Property<int>("VrstaPrinteraID");

                    b.HasKey("PrinterID");

                    b.HasIndex("LokacijaID");

                    b.HasIndex("VrstaPrinteraID");

                    b.ToTable("Printer");
                });

            modelBuilder.Entity("ServisProjekt.Models.Skladište", b =>
                {
                    b.Property<int>("SkladišteID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivSkladišta");

                    b.HasKey("SkladišteID");

                    b.ToTable("Skladište");
                });

            modelBuilder.Entity("ServisProjekt.Models.StatusNarudžbe", b =>
                {
                    b.Property<int>("StatusNarudžbeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OpisStatusa");

                    b.HasKey("StatusNarudžbeID");

                    b.ToTable("StatusNarudžbe");
                });

            modelBuilder.Entity("ServisProjekt.Models.VrstaNarudžbe", b =>
                {
                    b.Property<int>("VrstaNarudžbeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.HasKey("VrstaNarudžbeID");

                    b.ToTable("VrstaNarudžbe");
                });

            modelBuilder.Entity("ServisProjekt.Models.VrstaPredmeta", b =>
                {
                    b.Property<int>("VrstaPredmetaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Vrsta");

                    b.HasKey("VrstaPredmetaID");

                    b.ToTable("VrstaPredmeta");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ServisProjekt.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ServisProjekt.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ServisProjekt.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServisProjekt.Models.ApplicationUser", b =>
                {
                    b.HasOne("ServisProjekt.Models.Poduzeće", "Poduzeće")
                        .WithMany()
                        .HasForeignKey("PoduzećeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServisProjekt.Models.Narudžba", b =>
                {
                    b.HasOne("ServisProjekt.Models.ApplicationUser", "Naručitelj")
                        .WithMany()
                        .HasForeignKey("NaručiteljID");

                    b.HasOne("ServisProjekt.Models.Poduzeće", "Poduzeće")
                        .WithMany()
                        .HasForeignKey("PoduzećeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.Printer", "Printer")
                        .WithMany()
                        .HasForeignKey("PrinterID");

                    b.HasOne("ServisProjekt.Models.StatusNarudžbe", "StatusNarudžbe")
                        .WithMany()
                        .HasForeignKey("StatusNarudžbeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.VrstaNarudžbe", "VrstaNarudžbe")
                        .WithMany()
                        .HasForeignKey("VrstaNarudžbeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServisProjekt.Models.NarudžbaToPredmet", b =>
                {
                    b.HasOne("ServisProjekt.Models.Narudžba", "Narudžbe")
                        .WithMany()
                        .HasForeignKey("NarudžbeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.Predmet", "Predmeti")
                        .WithMany()
                        .HasForeignKey("PredmetiID");
                });

            modelBuilder.Entity("ServisProjekt.Models.Predmet", b =>
                {
                    b.HasOne("ServisProjekt.Models.ObiteljPrintera", "Printer")
                        .WithMany()
                        .HasForeignKey("PrinterID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.Skladište", "Skladište")
                        .WithMany()
                        .HasForeignKey("SkladišteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.VrstaPredmeta", "VrstaPredmeta")
                        .WithMany()
                        .HasForeignKey("VrstaPredmetaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServisProjekt.Models.Printer", b =>
                {
                    b.HasOne("ServisProjekt.Models.Poduzeće", "Lokacija")
                        .WithMany()
                        .HasForeignKey("LokacijaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServisProjekt.Models.ObiteljPrintera", "VrstaPrintera")
                        .WithMany()
                        .HasForeignKey("VrstaPrinteraID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
