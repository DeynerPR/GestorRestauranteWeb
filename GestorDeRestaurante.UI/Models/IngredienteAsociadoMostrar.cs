using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Models
{
    public class IngredienteAsociadoMostrar
    {
        public int Id { get; set; }

        [Display(Name = "Ingrediente")]
        public string NombreIngrediente { get; set; }
        public int Cantidad { get; set; }

        [Display(Name = "Medida")]
        public string NombreMedida { get; set; }

        [Display(Name = "Valor Aproximado")]
        public int ValorAproximado { get; set; }

    }
}
