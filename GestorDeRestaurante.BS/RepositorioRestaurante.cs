

using GestorDeRestaurante.Model;
using Microsoft.Extensions.Caching.Memory;

namespace GestorDeRestaurante.BS
{
    public class RepositorioRestaurante : IRepositorioRestaurante
    {



        private readonly IMemoryCache ElCache;
        private DA.DbContexto ElContexto;

        int idSeleccionado;



        //Inicializaciones
        //::::::::::::::::::::::::::::::::::::::
        public RepositorioRestaurante(IMemoryCache cache, DA.DbContexto contexto)
        {
            ElCache = cache;
            ElContexto = contexto;

            /*
            if (ElCache.Get("idSeleccionado") == null)
            {
                ElCache.Set("idSeleccionado", idSeleccionado);
            }
           */

        }//Fin constructor.





        public void AgregueLaMedida(Medida medida)
        {
            throw new NotImplementedException();
        }

        public void EditarMedida(Medida medida)
        {
            throw new NotImplementedException();
        }

        public Medida ObtenerMedidaPorId(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Medida> ObtengaLaListaDeMedidas()
        {
            throw new NotImplementedException();
        }

        public Medida ObtengaLaMedida(int Id)
        {
            throw new NotImplementedException();
        }















        public bool AgregarNuevoIngrediente(Ingrediente elIngrediente)
        {

            bool existe = VerifiqueSiExisteIngrediente(elIngrediente.Nombre);

            if (existe == false)
            {
                ElContexto.Ingredientes.Add(elIngrediente);
                ElContexto.SaveChanges();
            }//Fin if

            return existe;
        }//Fin metodo

        private bool VerifiqueSiExisteIngrediente(string nombre)
        {
            bool existe; existe = false;
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientesGeneral();

            foreach (Ingrediente esteIngrediente in losIngredientes)
            {
                if (esteIngrediente.Nombre.Equals(nombre))
                {
                    existe = true;
                    break;
                }//Fin if
            }//Fin foreach

            return existe;
        }//Fin metodo



        public List<Ingrediente> ObtengaLaListaDeIngredientesGeneral()
        {
            var resultado = from c in ElContexto.Ingredientes select c;
            List<Ingrediente> losIngredientes = resultado.ToList();
            return losIngredientes;
        }//Fin metodo

        public List<Medida> ObtengaLaListaDeMedidasGeneral()
        {
            var resultado = from c in ElContexto.Medidas select c;
            List<Medida> lasMedidas = resultado.ToList();
            return lasMedidas;
        }//Fin metodo


        public List<DetalleDePlatillo> ObtengaLosPlatillosQueUsanEsteIngrediente(int idDelIngrendienteElegido)
        {
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientesGeneral();
            List<Medida> lasMedidas = ObtengaLaListaDeMedidasGeneral();


            List<DetalleDePlatillo> losPlatillosQueTienenElIngrediente = new List<DetalleDePlatillo>();
            DetalleDePlatillo elPlatilloADetallar;

            var resultado = from c in ElContexto.MenuIngredientes select c;
            List<PlatilloIngredientes> todosLosPlatillosDelMenu = resultado.ToList();

            foreach (PlatilloIngredientes estePlatillo in todosLosPlatillosDelMenu)
            {
                if (estePlatillo.Id_Ingredientes == idDelIngrendienteElegido)
                {
                    elPlatilloADetallar = new DetalleDePlatillo();
                    elPlatilloADetallar.NombrePlatillo = DemeElNombre(idDelIngrendienteElegido, losIngredientes);
                    elPlatilloADetallar.NombreMedida = DemeElNombreDeLaMedida(estePlatillo.Id_Medidas, lasMedidas);
                    elPlatilloADetallar.Cantidad = estePlatillo.Cantidad.ToString();
                    losPlatillosQueTienenElIngrediente.Add(elPlatilloADetallar);
                }
            }//Fin foreach

            return losPlatillosQueTienenElIngrediente;
        }//Fin metodo


        private string DemeElNombre(int idDelIngrendienteElegido, List<Ingrediente> losIngredientes)
        {
            string elNombre = "---";
            foreach (Ingrediente esteIngrediente in losIngredientes)
            {
                if (esteIngrediente.Id == idDelIngrendienteElegido)
                {
                    elNombre = esteIngrediente.Nombre;
                }
            }//FiN
            return elNombre;
        }//FIn metodo


        private string DemeElNombreDeLaMedida(int idDeLaMedidaEnPlatillo, List<Medida> lasMedidas)
        {
            string elNombre = "---";
            foreach (Medida estaMedida in lasMedidas)
            {
                if (estaMedida.Id == idDeLaMedidaEnPlatillo)
                {
                    elNombre = estaMedida.Nombre;
                }
            }//FiN
            return elNombre;
        }//FIn metodo

    }
}

