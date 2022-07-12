using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class MenuIngredienteAsociar
    {

        public List<String>? laListaDeIngredientes { get; set; }
        public List<String>? laListaDeMedidas { get; set; }

        public String? elIngredienteSeleccionado { get; set; }
        public String? laMedidaSeleccionada { get; set; }



        public int Id_menu { get; set; }

        

        
        
        [Required(ErrorMessage = "Campo requerido")]
        public double Cantidad { get; set; }
        
        
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Valor Aproximado")]
        public int ValorAproximado { get; set; }

    }
}
