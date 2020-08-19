using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.ServiserViewModel
{
    public class OtpisPredmetaModel
    {
        public int Količina { get; set; }
        public string PredmetID { get; set; }
        public List<SelectListItem> Predmet { get; set; }
    }
}
