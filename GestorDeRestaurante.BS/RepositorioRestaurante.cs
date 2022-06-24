using GestorDeRestaurante.Model;
using Microsoft.Extensions.Caching.Memory;

namespace GestorDeRestaurante.BS
{
    public class RepositorioRestaurante : IRepositorioRestaurante
    {

        private readonly IMemoryCache ElCache;
        private DA.DbContexto ElContextoBD;

        int idSeleccionado;

        //Inicializaciones
        //::::::::::::::::::::::::::::::::::::::
        public RepositorioRestaurante(IMemoryCache cache, DA.DbContexto contexto)
        {
            ElCache = cache;
            ElContextoBD = contexto;

            /*
            if (ElCache.Get("idSeleccionado") == null)
            {
                ElCache.Set("idSeleccionado", idSeleccionado);
            }
           */

        }//Fin constructor.


        //MEDIDAS
        public List<Medida> ObtengaLaListaDeMedidas()
        {
            var resultado = from c in ElContextoBD.Medidas select c;
            List<Medida> lasMedidas = resultado.ToList();

            return lasMedidas;

        }

        public bool ExisteLaMedida(string nombre)
        {
            bool existe; existe = false;
            List<Medida> lasMedidas = ObtengaLaListaDeMedidas();

            foreach (Medida medida in lasMedidas)
            {
                if (medida.Nombre.Equals(nombre))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        public void AgregueLaMedida(Medida laMedida)
        {

            bool existe = ExisteLaMedida(laMedida.Nombre);

            if (existe == false)
            {
                ElContextoBD.Medidas.Add(laMedida);
                ElContextoBD.SaveChanges();

            }
        }

        public Medida ObtenerMedidaPorId(int Id)
        {
            Model.Medida laMedida;

            laMedida = ElContextoBD.Medidas.Find(Id);

            return laMedida;

        }

        public void EditarMedida(Medida medida)
        {
            Model.Medida laMedidaAModificar;

            laMedidaAModificar = ObtenerMedidaPorId(medida.Id);

            laMedidaAModificar.Nombre = medida.Nombre;

            ElContextoBD.Medidas.Update(laMedidaAModificar);
            ElContextoBD.SaveChanges();

        }

        public Medida ObtengaLaMedida(int Id)
        {
            throw new NotImplementedException();
        }

        public string ObtengaElNombreDeLaMedida(int idMedidaEnPlatillo, List<Medida> lasMedidas)
        {
            string elNombre = "---";

            foreach (Medida estaMedida in lasMedidas)
            {
                if (estaMedida.Id == idMedidaEnPlatillo)
                {
                    elNombre = estaMedida.Nombre;
                }
            }

            return elNombre;
        }





        //INGREDIENTES
        public List<Ingrediente> ObtengaLaListaDeIngredientes()
        {
            var resultado = from c in ElContextoBD.Ingredientes select c;
            List<Ingrediente> losIngredientes = resultado.ToList();

            return losIngredientes;

        }//Fin metodo

        public bool ExisteElIngrediente(string nombre)
        {
            bool existe; existe = false;
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientes();

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

        public void AgregueElIngrediente(Ingrediente elIngrediente)
        {

            bool existe = ExisteElIngrediente(elIngrediente.Nombre);

            if (existe == false)
            {
                ElContextoBD.Ingredientes.Add(elIngrediente);
                ElContextoBD.SaveChanges();
            }//Fin if
        }//Fin metodo

        public Ingrediente ObtenerIngredientePorId(int Id)
        {
            Model.Ingrediente elIngrediente;

            elIngrediente = ElContextoBD.Ingredientes.Find(Id);

            return elIngrediente;

        }

        public void EditarIngrediente(Ingrediente ingrediente)
        {
            Model.Ingrediente elIngredienteAModificar;

            elIngredienteAModificar = ObtenerIngredientePorId(ingrediente.Id);

            elIngredienteAModificar.Nombre = ingrediente.Nombre;

            ElContextoBD.Ingredientes.Update(elIngredienteAModificar);
            ElContextoBD.SaveChanges();

        }

        public List<DetalleDePlatillo> ObtengaLosPlatillosQueUsanEsteIngrediente(int idIngredienteElegido)
        {
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientes();
            List<Medida> lasMedidas = ObtengaLaListaDeMedidas();


            List<DetalleDePlatillo> losPlatillosQueTienenElIngrediente = new List<DetalleDePlatillo>();
            DetalleDePlatillo elPlatilloADetallar;

            var resultado = from c in ElContextoBD.MenuIngredientes select c;
            List<PlatilloIngredientes> todosLosPlatillosDelMenu = resultado.ToList();

            foreach (PlatilloIngredientes estePlatillo in todosLosPlatillosDelMenu)
            {
                if (estePlatillo.Id_Ingredientes == idIngredienteElegido)
                {
                    elPlatilloADetallar = new DetalleDePlatillo();
                    elPlatilloADetallar.NombrePlatillo = ObtengaElNombreDelIngrediente(idIngredienteElegido, losIngredientes);
                    elPlatilloADetallar.NombreMedida = ObtengaElNombreDeLaMedida(estePlatillo.Id_Medidas, lasMedidas);
                    elPlatilloADetallar.Cantidad = estePlatillo.Cantidad.ToString();
                    losPlatillosQueTienenElIngrediente.Add(elPlatilloADetallar);
                }
            }//Fin foreach

            return losPlatillosQueTienenElIngrediente;
        }//Fin metodo


        public string ObtengaElNombreDelIngrediente(int idIngredienteElegido, List<Ingrediente> losIngredientes)
        {
            string elNombre = "---";

            foreach (Ingrediente esteIngrediente in losIngredientes)
            {
                if (esteIngrediente.Id == idIngredienteElegido)
                {
                    elNombre = esteIngrediente.Nombre;
                }
            }//FiN
            return elNombre;
        }//FIn metodo




    }// FIN CLASE
}

