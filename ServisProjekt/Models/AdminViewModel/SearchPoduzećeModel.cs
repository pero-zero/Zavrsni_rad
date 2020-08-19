using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.AdminViewModel
{
    public class SearchPoduzećeModel
    {
        public int PoduzećeID { get; set; }
        public bool IsChanged { get; set; }
        public List<SelectListItem> PoduzećePopis { get; set; }
    }
}
