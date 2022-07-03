using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class DetalleDelIngrediente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<DetalleDePlatillo> losPlatillosConDetalle { get; set; }

        public DetalleDelIngrediente()
        {
            losPlatillosConDetalle = new List<DetalleDePlatillo>();
        }


        


    }
}
