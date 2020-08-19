using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class RadniNalogModel
    {
        public int RadniNalogID { get; set; }
        public string Opis { get; set; }
        [Display(Name = "Vrijeme Dolaska")]
        public DateTime VrijemeDolaska { get; set; }
        [Display(Name = "Vrijeme Odlaska")]
        public DateTime VrijemeOdlaska { get; set; }
        public int Counter { get; set; }
        public string Serviser { get; set; }
    }
}
