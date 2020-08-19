using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class StatusNarudžbe
    {
        [Key]
        public int StatusNarudžbeID { get; set; }
        public string OpisStatusa { get; set; }
    }
}
