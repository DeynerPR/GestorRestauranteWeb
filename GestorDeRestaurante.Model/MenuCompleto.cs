using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class MenuCompleto
    {
        public MenuCompleto()
        {
            Entradas = new List<Platillo>();
            PequenasBotanas = new List<Platillo>();
            Aperitivos = new List<Platillo>();
            SopasYEnsaladas = new List<Platillo>();
            Principales = new List<Platillo>();
            Postres = new List<Platillo>();
            Bebidas = new List<Platillo>();
        }//Fin constructor

        public List<Platillo> Entradas { get; set; }

        public List<Platillo> PequenasBotanas { get; set; }

        public List<Platillo> Aperitivos { get; set; }

        public List<Platillo> SopasYEnsaladas { get; set; }

        public List<Platillo> Principales { get; set; }

        public List<Platillo> Postres { get; set; }

        public List<Platillo> Bebidas { get; set; }




    }//Fin class
}
