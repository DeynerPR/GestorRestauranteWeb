using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.UI.Models
{
    public class MenuIngredienteAsociar
    {
        public int Id { get; set; }

        [Display(Name = "Ingrediente")]
        public string NombreIngrediente { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public double Cantidad { get; set; }

        [Display(Name = "Medida")]
        public string NombreMedida { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Valor Aproximado")]
        public int ValorAproximado { get; set; }

    }
}
