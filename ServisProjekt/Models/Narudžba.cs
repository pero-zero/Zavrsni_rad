using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class Narudžba
    {
        public int NarudžbaID { get; set; }
        public DateTime VrijemeNaruđbe { get; set; }
        public string Opis { get; set; }
        public int Counteri { get; set; }
        public string ŠifraGreške { get; set; }
        [ForeignKey("StatusNarudžbe")]
        public int StatusNarudžbeID { get; set; }
        [ForeignKey ("Poduzeće")]
        public int PoduzećeID { get; set; }
        [ForeignKey("Naručitelj")]
        public string NaručiteljID { get; set; }
        [ForeignKey("Printer")]
        public string PrinterID { get; set; }
        [ForeignKey("VrstaNarudžbe")]
        public int VrstaNarudžbeID { get; set; }
        public virtual StatusNarudžbe StatusNarudžbe { get; set; }
        public virtual Poduzeće Poduzeće { get; set; }
        public virtual Printer Printer { get; set; }
        public virtual VrstaNarudžbe VrstaNarudžbe { get; set; }
        public virtual ApplicationUser Naručitelj { get; set; }
    }
}
