
using System.ComponentModel.DataAnnotations;

namespace GestorDeRestaurante.UI.Models
{
    public class Mesa
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }

        public Estado Estado { get; set; }

    }
}
