using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class Printer
    {
        public string PrinterID { get; set; }
        public string NazivModela { get; set; }
        [ForeignKey("Lokacija")]
        public int LokacijaID { get; set; }
        [ForeignKey("VrstaPrintera")]
        public int VrstaPrinteraID { get; set; }

        public virtual Poduzeće Lokacija { get; set; }
        public virtual ObiteljPrintera VrstaPrintera { get; set; }
    }
}
