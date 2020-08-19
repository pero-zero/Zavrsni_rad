using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class PopisKorisnikaModel
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [Display(Name = "Broj telefona")]
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public string Poduzeće { get; set; }
    }
}
