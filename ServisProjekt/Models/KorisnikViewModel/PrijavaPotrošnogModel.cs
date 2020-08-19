using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.KorisnikViewModel
{
    public class PrijavaPotrošnogModel
    {
        public ObiteljPrinteraSearchModel ObiteljPrintera { get; set; }
        public List<DodajPredmetModel> Potrošni { get; set; }
        public string Opis { get; set; }
    }
}
