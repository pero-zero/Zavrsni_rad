using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class RadniNalog
    {
        public int RadniNalogID { get; set; }
        public string Opis { get; set; }
        public DateTime VrijemeDolaska { get; set; }
        public DateTime VrijemeOdlaska { get; set; }
        public DateTime Datum { get; set; }
        public int Counter { get; set; }
        [ForeignKey("Narudžba")]
        public int NarudžbaID { get; set; }
        [ForeignKey("Korisnik")]
        public string KorisnikID { get; set; }
        public virtual Narudžba Narudžba{ get; set; }
        public virtual ApplicationUser Korisnik { get; set; }
    }
}
