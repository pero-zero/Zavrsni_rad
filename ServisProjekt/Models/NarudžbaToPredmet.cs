using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class NarudžbaToPredmet
    {
        [Key]
        public int NarudžbaToPredmetID { get; set; }
        public int Količina { get; set; }
        [ForeignKey("Predmeti")]
        public string PredmetiID { get; set; }
        public Predmet Predmeti { get; set; }
        [ForeignKey("Narudžbe")]
        public int NarudžbeID { get; set; }
        public virtual Narudžba Narudžbe { get; set; }
    }
}
