using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class VrstaPredmeta
    {
        [Key]
        public int VrstaPredmetaID { get; set; }
        public string Vrsta { get; set; } //potrošni, serviserski
    }
}
