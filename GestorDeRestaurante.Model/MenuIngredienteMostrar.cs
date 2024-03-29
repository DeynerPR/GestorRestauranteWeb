﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class MenuIngredienteMostrar
    {
        public int Id { get; set; }
        public int Id_Menu { get; set; }
        public int Id_Ingredientes { get; set; }
        public string NombreIngrediente { get; set; }
        public double Cantidad { get; set; }
        public int Id_Medidas { get; set; }
        public string NombreMedida { get; set; }

        [Display(Name = "Valor Aproximado")]
        public int ValorAproximado { get; set; }

    }
}
