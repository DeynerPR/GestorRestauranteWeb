using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class DetalleDePlatillo
    {
        [Display(Name = "Nombre")]
        public string NombrePlatillo { get; set; }


        [Display(Name = "Nombre de la medida")]
        public string NombreMedida { get; set; }

        public string Cantidad { get; set; }
    }
}
