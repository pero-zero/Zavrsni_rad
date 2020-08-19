using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.ServiserViewModel
{ 
    public class PopisPrijavaISearchPoPrinteruIPoduzećuModel
    {
        public IEnumerable<PopisNarudžbiModel> PopisNarudžbi { get; set; }
        public SearchPrinterModel PopisPrintera { get; set; }
        public SearchPoduzećeModel PopisPoduzeća { get; set; }
    }
}
