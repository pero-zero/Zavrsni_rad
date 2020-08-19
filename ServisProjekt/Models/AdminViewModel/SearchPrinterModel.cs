using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class SearchPrinterModel
    {
        public string PrinterID { get; set; }
        public List<SelectListItem> PrinterPopis { get; set; }
    }
}
