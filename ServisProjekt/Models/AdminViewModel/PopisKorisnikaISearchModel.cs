using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class PopisKorisnikaISearchModel
    {
        public IEnumerable<PopisKorisnikaModel> PopisKorisnika { get; set; }
        public SearchPoduzećeModel PopisPoduzeća { get; set; }
    }
}
