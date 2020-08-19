using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class DodajPredmetModel
    {
        
        public string PredmetID { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public string Dobavljač { get; set; }
        public int MinimalnaKoličina { get; set; }
        public int Količina { get; set; }
        public int ObiteljPrinteraID { get; set; }
        public int VrstaPredmetaID { get; set; }
        [Display(Name ="Obitelj printera")]
        public List<SelectListItem> ObiteljPrintera { get; set; }
        [Display(Name ="Vrsta predmeta")]
        public List<SelectListItem> VrstaPredmeta { get; set; }
        public string Error { get; set; }
    }
}
