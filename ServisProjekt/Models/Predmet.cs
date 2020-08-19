using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models
{
    public class Predmet
    {
        [Key]
        [Required]
        public string PredmetID { get; set; } //Serijski broj predmeta
        [Required]
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public string Dobavljač { get; set; }
        [Required]
        public int MinimalnaKoličina { get; set; }
        public int Količina { get; set; }
        
        [ForeignKey("Skladište")]
        public int SkladišteID { get; set; }
        
        [ForeignKey("Printer")]
        public int PrinterID { get; set; }
        
        [ForeignKey("VrstaPredmeta")]
        public int VrstaPredmetaID { get; set; }

        public virtual Skladište Skladište { get; set; }
        public virtual ObiteljPrintera Printer { get; set; }
        public virtual VrstaPredmeta VrstaPredmeta { get; set; }
    }
}
