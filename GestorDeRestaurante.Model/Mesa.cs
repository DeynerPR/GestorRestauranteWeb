using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class Mesa
    {
        public Mesa()
        {
            OrdenesAsociadas = new List<Orden>();
        }//Fin constructor

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }

        public List<Model.Orden> OrdenesAsociadas { get; set; }
    }
}
