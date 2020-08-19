using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServisProjekt.Models;

namespace ServisProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            
        }

        public DbSet<ServisProjekt.Models.Narudžba> Narudžba { get; set; }
        public DbSet<ServisProjekt.Models.Poduzeće> Poduzeće { get; set; }
        public DbSet<ServisProjekt.Models.Predmet> Predmet { get; set; }
        public DbSet<ServisProjekt.Models.NarudžbaToPredmet> NarudžbaToPredmet { get; set; }
        public DbSet<ServisProjekt.Models.ObiteljPrintera> ObiteljPrintera { get; set; }
        public DbSet<ServisProjekt.Models.Printer> Printer { get; set; }
        public DbSet<ServisProjekt.Models.Skladište> Skladište { get; set; }
        public DbSet<ServisProjekt.Models.VrstaNarudžbe> VrstaNarudžbe { get; set; }
        public DbSet<ServisProjekt.Models.VrstaPredmeta> VrstaPredmeta { get; set; }
        public DbSet<ServisProjekt.Models.StatusNarudžbe> StatusNarudžbe { get; set; }
        public DbSet<ServisProjekt.Models.RadniNalog> RadniNalog { get; set; }
        public DbSet<ServisProjekt.Models.OtpisSaSkladišta> OtpisSaSkladišta { get; set; }
        

    }
}
