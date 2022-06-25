using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.UI.Models
{
    public class PlatilloIngredientesMostrar
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Precio { get; set; }

        [Display(Name = "Ganancia Aproximada")]
        public int Ganancia { get; set; }

    }
}
