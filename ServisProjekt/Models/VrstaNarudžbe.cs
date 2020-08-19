using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class VrstaNarudžbe
    {
        [Key]
        public int VrstaNarudžbeID { get; set; }
        public string Naziv { get; set; }
    }
}
