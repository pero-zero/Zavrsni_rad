using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class OtpisSaSkladišta
    {
        public int OtpisSaSkladištaID { get; set; }
        public int Kolicina { get; set; }
        [ForeignKey("RadniNalog")]
        public int RadniNalogID { get; set; }
        [ForeignKey("Predmet")]
        public string PredmetID { get; set; }
        public virtual RadniNalog RadniNalog { get; set; }
        public virtual Predmet Predmet { get; set; }
    }
}
