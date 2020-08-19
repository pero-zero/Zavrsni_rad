using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.KorisnikViewModel
{
    public class ObiteljPrinteraSearchModel
    {
        public int ObiteljPrinteraID { get; set; }
        [Display(Name = "Serija printera")]
        public List<SelectListItem> SerijaPrintera { get; set; }
    }
}
