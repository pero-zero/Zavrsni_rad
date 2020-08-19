using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class ObiteljPrintera
    {
        [Key]
        public int ObiteljPrinteraID { get; set; }
        public string Obitelj { get; set; } //serije printera
    }
}
