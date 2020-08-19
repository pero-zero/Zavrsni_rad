using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class PopisNarudžbiModel
    {
        public int NarudžbaID { get; set; }
        public string Poduzeće { get; set; }
        public string Naručitelj { get; set; }
        [Display(Name = "Vrsta narudžbe")]
        public string VrstaNarudžbe { get; set; }
        [Display(Name = "Serijski broj printera")]
        public string SerijskiBrojPrintera { get; set; }
        [Display(Name = "Vrijeme narudžbe")]
        public DateTime VrijemeNarudžbe { get; set; }
        public string Opis { get; set; }
        [Display(Name = "Status narudžbe")]
        public string StatusNarudžbe { get; set; }
        public int Counter { get; set; }
        [Display(Name = "Šifra greške")]
        public string ŠifraGreške { get; set; }
    }
}
