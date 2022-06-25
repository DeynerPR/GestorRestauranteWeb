using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.UI.Models
{
    public class PlatilloIngredientes
    {
        public int Id { get; set; }

        public int PlatilloId { get; set; }

        public int IngredienteId { get; set; }

        public int MedidaId { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Valor Aproximado")]
        public int ValorAproximado { get; set; }


    }
}
