using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.UI.Models
{
    public class Platillo
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Categoría")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Range(0, float.MaxValue, ErrorMessage = "Ingrese un valor válido")]
        public double Precio { get; set; }


        [Required(ErrorMessage = "Pon una Imagen")]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Imagen { get; set; }

    }
}
