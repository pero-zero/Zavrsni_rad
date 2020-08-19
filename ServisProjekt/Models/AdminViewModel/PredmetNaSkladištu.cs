using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class PredmetNaSkladištu
    {
        public string PredmetID { get; set; }
        public string Naziv { get; set; }
        public int KolicinaNaSkladištu { get; set; }
        public string Skladište { get; set; }
        [Display(Name = "Obitelj Printera")]
        public string ObiteljPrintera { get; set; }
    }
}
