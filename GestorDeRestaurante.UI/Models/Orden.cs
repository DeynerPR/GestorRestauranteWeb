using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.UI.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public int Id_Mesa { get; set; }
        public int Id_Menu { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int Cantidad { get; set; }


    }
}
