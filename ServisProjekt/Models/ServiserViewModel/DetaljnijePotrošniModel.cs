﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisProjekt.Models.ServiserViewModel
{
    public class DetaljnijePotrošniModel
    {
        public PopisNarudžbiModel Narudžba { get; set; }
        public List<DetaljnijePredmetModel> Potrošni { get; set; }
    }
}
