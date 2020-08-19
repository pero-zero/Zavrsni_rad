using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.ServiserViewModel
{
    public class NalogPredmetNarudžbaModel
    {
        public bool DodajJoš { get; set; }
        public int Narudžba { get; set; }
        public List<OtpisPredmetaModel> Otpis { get; set; }
        public RadniNalogModel Nalog { get; set; }
    }
}
