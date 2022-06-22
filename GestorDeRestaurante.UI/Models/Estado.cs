﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Models
{
    public enum Estado
    {
        Disponible = 1,

        [Display(Name = "No Disponible")]
        NoDisponible = 2
    }
}
