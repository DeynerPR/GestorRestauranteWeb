using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.UI.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }

    }
}
