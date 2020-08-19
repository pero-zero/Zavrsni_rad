using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class DetaljnijeRadniNalogModel
    {
        public RadniNalogModel RadniNalog { get; set; }
        public List<PopisPredmetaRNModel> PopisPredmeta { get; set; } 
    }
}
