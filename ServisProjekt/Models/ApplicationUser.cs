using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [ForeignKey("Poduzeće")]
        public int PoduzećeID { get; set; }
        public virtual Poduzeće Poduzeće { get; set; }
        public bool PrviLogin { get; set; }
        public string Password { get; set; }
    }
}
