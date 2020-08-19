using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.KorisnikViewModel
{
    public class DetaljnijePotrošniModel
    {
        public PopisNarudžbiModel Narudžba { get; set; }
        public List<DodajPredmetModel> Potrošni { get; set; }
    }
}
