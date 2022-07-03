using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class PlatilloIngredientesMostrar
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        [Display(Name = "Ganancia Aproximada")]
        public double Ganancia { get; set; }

        public List<Model.MenuIngredienteMostrar> ListaDeIngredientes { get; set; }

    }
}
