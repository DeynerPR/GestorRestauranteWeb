using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Models
{
    public class Platillo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Categoría")]
        public Categoria Categoria { get; set; }
        public float Precio { get; set; }
        public byte[] Imagen { get; set; }

    }
}
