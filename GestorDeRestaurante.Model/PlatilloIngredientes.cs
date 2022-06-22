using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class PlatilloIngredientes
    {
        public int Id { get; set; }
        public int PlatilloId { get; set; }
        public int IngredienteId { get; set; }
        public int MedidaId { get; set; }

        public int Cantidad { get; set; }

        [Display(Name = "Valor Aproximado")]
        public int ValorAproximado { get; set; }

    }
}
