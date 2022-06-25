using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class Orden
    {
        public int Id { get; set; }
        public int Id_Mesa { get; set; }
        public int Id_Menu { get; set; }
        public int Cantidad { get; set; }
        public EstadoOrden Estado { get; set; }

    }
}
