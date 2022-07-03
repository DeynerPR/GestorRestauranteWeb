using GestorDeRestaurante.Model;
using Microsoft.Extensions.Caching.Memory;

namespace GestorDeRestaurante.BS
{
    public class RepositorioRestaurante : IRepositorioRestaurante
    {

        private DA.DbContexto ElContextoBD;

        //Inicializaciones::::::::::::::::::::::::::::::::::::::::::.
        public RepositorioRestaurante(DA.DbContexto contexto)
        {
            ElContextoBD = contexto;
            CargarMedidasPorDefecto();

        }



        //MEDIDAS::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::..

        //      -------Publicos------
        public List<Medida> ObtengaLaListaDeMedidas()
        {
            var resultado = from c in ElContextoBD.Medidas select c;
            List<Medida> lasMedidas = resultado.ToList();
            return lasMedidas;
        }//Fin metodo

        public void AgregueLaMedida(Medida laMedida)
        {

            bool existe = ExisteLaMedida(laMedida.Nombre);

            if (existe == false)
            {
                ElContextoBD.Medidas.Add(laMedida);
                ElContextoBD.SaveChanges();

            }
        }//Fin metodo

        public void EditarMedida(Medida medida)
        {
            Model.Medida laMedidaAModificar;

            laMedidaAModificar = ObtenerMedidaPorId(medida.Id);

            laMedidaAModificar.Nombre = medida.Nombre;

            ElContextoBD.Medidas.Update(laMedidaAModificar);
            ElContextoBD.SaveChanges();

        }//Fin metodo

        public Medida ObtenerMedidaPorId(int Id)
        {
            Model.Medida laMedida;

            laMedida = ElContextoBD.Medidas.Find(Id);

            return laMedida;

        }//Fin metodo


        //      -------Privados------
        private string ObtengaElNombreDeLaMedida(int idMedidaEnPlatillo, List<Medida> lasMedidas)
        {
            string elNombre = "---";

            foreach (Medida estaMedida in lasMedidas)
            {
                if (estaMedida.Id == idMedidaEnPlatillo)
                {
                    elNombre = estaMedida.Nombre;
                    break;
                }
            }

            return elNombre;

        }//Fin metodo
        
        private bool ExisteLaMedida(string nombre)
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

        }//Fin metodo
        
        public void CargarMedidasPorDefecto()
        {

            List<Medida> lasMedidas = ObtengaLaListaDeMedidas();
            List<string> losNombres = new List<string>() { "Pizca", "Cucharada", "Taza", "Litro", "Mililitros" };

            Medida laNuevaMedida;
            if (lasMedidas.Count == 0)
            {
                foreach (var esteNombre in losNombres)
                {
                    laNuevaMedida = new Medida();
                    laNuevaMedida.Nombre = esteNombre;
                    ElContextoBD.Medidas.Add(laNuevaMedida);
                    ElContextoBD.SaveChanges();
                }
            }//Fin if
        }//Fin metodo






        //Ingredientes::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        //      -------Publicos------
        public List<Ingrediente> ObtengaLaListaDeIngredientes()
        {
            var resultado = from c in ElContextoBD.Ingredientes select c;
            List<Ingrediente> losIngredientes = resultado.ToList();

            return losIngredientes;

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

        public void EditarIngrediente(Ingrediente ingrediente)
        {
            Model.Ingrediente elIngredienteAModificar;

            elIngredienteAModificar = ObtenerIngredientePorId(ingrediente.Id);

            elIngredienteAModificar.Nombre = ingrediente.Nombre;

            ElContextoBD.Ingredientes.Update(elIngredienteAModificar);
            ElContextoBD.SaveChanges();

        }//Fin metodo

        public DetalleDelIngrediente ObtengaElDetalleDelIngrediente(int idIngredienteElegido)
        {
            DetalleDelIngrediente elDetalleDelIngrediente = new DetalleDelIngrediente();
            List<DetalleDePlatillo> losPlatillosQueTienenElIngrediente = new List<DetalleDePlatillo>();
            
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientes();
            List<Medida> lasMedidas = ObtengaLaListaDeMedidas();
            List<Platillo> losPlatillos = ObtengaLosPlatillosDelMenu();
            elDetalleDelIngrediente.Id = idIngredienteElegido;
            elDetalleDelIngrediente.Nombre = ObtengaElNombreDelIngrediente(idIngredienteElegido, losIngredientes);


            var resultado = from c in ElContextoBD.MenuIngredientes select c;
            List<MenuIngrediente> todosLosPlatillosDelMenu = resultado.ToList();
            DetalleDePlatillo elPlatilloADetallar;

            foreach (MenuIngrediente estePlatillo in todosLosPlatillosDelMenu)
            {
                if (estePlatillo.Id_Ingredientes == idIngredienteElegido)
                {
                    elPlatilloADetallar = new DetalleDePlatillo();
                    elPlatilloADetallar.NombrePlatillo = ObtengaElNombreDePlatillo(estePlatillo.Id_Menu, losPlatillos);
                    elPlatilloADetallar.NombreMedida = ObtengaElNombreDeLaMedida(estePlatillo.Id_Medidas, lasMedidas);
                    elPlatilloADetallar.Cantidad = estePlatillo.Cantidad.ToString();
                    losPlatillosQueTienenElIngrediente.Add(elPlatilloADetallar);
                }
            }
            elDetalleDelIngrediente.losPlatillosConDetalle = losPlatillosQueTienenElIngrediente;

            return elDetalleDelIngrediente;
        }//Fin metodo

        public Ingrediente ObtenerIngredientePorId(int Id)
        {
            Model.Ingrediente elIngrediente;

            elIngrediente = ElContextoBD.Ingredientes.Find(Id);

            return elIngrediente;

        }//Fin metodo


        //      -------Privados------
        private string ObtengaElNombreDelIngrediente(int idIngredienteElegido, List<Ingrediente> losIngredientes)
        {
            string elNombre = "---";

            foreach (Ingrediente esteIngrediente in losIngredientes)
            {
                if (esteIngrediente.Id == idIngredienteElegido)
                {
                    elNombre = esteIngrediente.Nombre;
                    break;
                }
            }//FiN
            return elNombre;
        }//FIn metodo

        private bool ExisteElIngrediente(string nombre)
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






        //Menu::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        //      -------Publicos------
        public List<Platillo> ObtengaLosPlatillosDelMenu()
        {
            var resultado = from c in ElContextoBD.Menu select c;
            List<Platillo> losPlatillos = resultado.ToList();

            return losPlatillos;
        }//Fin metodo

        public void AgregueElPlatillo(Platillo elPlatillo)
        {
            bool existe = ExisteElPlatillo(elPlatillo.Nombre);

            if (existe == false)
            {
                ElContextoBD.Menu.Add(elPlatillo);
                ElContextoBD.SaveChanges();
            }//Fin if
        }//Fin metodo

        public void EditarPlatillo(Platillo elPlatillo) {

            Model.Platillo elPlatilloAModificar = new Platillo();

            elPlatilloAModificar = ObtenerPlatilloPorId(elPlatillo.Id);

            elPlatilloAModificar.Nombre = elPlatillo.Nombre;
            elPlatilloAModificar.Categoria = elPlatillo.Categoria;
            elPlatilloAModificar.Precio = elPlatillo.Precio;
            elPlatilloAModificar.Imagen = elPlatillo.Imagen;

            ElContextoBD.Menu.Update(elPlatilloAModificar);
            ElContextoBD.SaveChanges();
        }//Fin metodo

        public Platillo ObtengaElDetalleDelPlatillo(int id)
        {
            Platillo elPlatillo = ElContextoBD.Menu.Find(id);
            return elPlatillo;

        }//Fin metodo

        public Platillo ObtenerPlatilloPorId(int id)
        {
            Model.Platillo Platillo;

            Platillo = ElContextoBD.Menu.Find(id);

            return Platillo;
        }//Fin metodo

        public MenuCompleto ObtenerMenuCompleto() {

            MenuCompleto elMenuCompleto = new MenuCompleto();
            List<Platillo> losPlatillos = ObtengaLosPlatillosDelMenu();
            int CategoriaDeMenu;

            foreach (var estePlatillo in losPlatillos)
            {
                CategoriaDeMenu = (int)estePlatillo.Categoria;
                
                switch (CategoriaDeMenu)
                {
                    case 1:
                        elMenuCompleto.Entradas.Add(estePlatillo);
                        break;

                    case 2:
                        elMenuCompleto.PequenasBotanas.Add(estePlatillo);
                        break;

                    case 3:
                        elMenuCompleto.Aperitivos.Add(estePlatillo);
                        break;

                    case 4:
                        elMenuCompleto.SopasYEnsaladas.Add(estePlatillo);
                        break;

                    case 5:
                        elMenuCompleto.Principales.Add(estePlatillo);
                        break;
                    
                    case 6:
                        elMenuCompleto.Postres.Add(estePlatillo);
                        break;
                    
                    case 7:
                        elMenuCompleto.Bebidas.Add(estePlatillo);
                        break;
                   
                }
            }

            return elMenuCompleto;
        }//Fin metodo


        //      -------Privados------
        private string ObtengaElNombreDePlatillo(int idDelMenu, List<Platillo> losPlatillos)
        {

            string elNombre = "---";

            foreach (Platillo estePlatillo in losPlatillos)
            {
                if (estePlatillo.Id == idDelMenu)
                {
                    elNombre = estePlatillo.Nombre;
                    break;
                }
            }
            return elNombre;
        }//Fin metodo

        private bool ExisteElPlatillo(string nombre)
        {
            bool existe; existe = false;
            List<Platillo> losPatillos = ObtengaLosPlatillosDelMenu();

            foreach (Platillo estePlatillo in losPatillos)
            {
                if (estePlatillo.Nombre.Equals(nombre))
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }//Fin metodo





        //IngredientesDelMenu::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        public List<PlatilloIngredientesMostrar> ConviertaLosPlatillosParaMostrar(List<Platillo> losPlatillos)
        {
            List<Model.PlatilloIngredientesMostrar> losPlatillosPorMostrar = new List<PlatilloIngredientesMostrar>();
            List<MenuIngrediente> losIngredientesDelMenu = ObtengaLaListaDeIngredientesDelMenu();
            List<MenuIngrediente> laListaDeIngredientesFiltrada = new();

            foreach (var item in losPlatillos)
            {
                Model.PlatilloIngredientesMostrar elPlatilloParaMostrar = new PlatilloIngredientesMostrar();

                elPlatilloParaMostrar.Id = item.Id;
                elPlatilloParaMostrar.Nombre = item.Nombre;
                elPlatilloParaMostrar.Precio = item.Precio;

                laListaDeIngredientesFiltrada = losIngredientesDelMenu.Where(x => x.Id_Menu == item.Id).ToList();
                int sumaValoresAproximados = laListaDeIngredientesFiltrada.Sum(item => item.ValorAproximado);

                elPlatilloParaMostrar.Ganancia = (double)elPlatilloParaMostrar.Precio - sumaValoresAproximados;

                losPlatillosPorMostrar.Add(elPlatilloParaMostrar);

            }

            return losPlatillosPorMostrar;
        }

        public PlatilloIngredientesMostrar ConviertaElPlatilloParaMostrar(Platillo elPlatillo)
        {

            Model.PlatilloIngredientesMostrar elPlatilloParaMostrar = new PlatilloIngredientesMostrar();

            elPlatilloParaMostrar.Id = elPlatillo.Id;
            elPlatilloParaMostrar.Nombre = elPlatillo.Nombre;
            elPlatilloParaMostrar.Precio = elPlatillo.Precio;
            elPlatilloParaMostrar.Ganancia = 0;

            return elPlatilloParaMostrar;
        }

        public List<MenuIngrediente> ObtengaLaListaDeIngredientesDelMenu()
        {

            var resultado = from c in ElContextoBD.MenuIngredientes select c;

            return resultado.ToList();

        }

        public PlatilloIngredientesMostrar ObtengaLaListaDeIngredientesDelPlatillo(int idPlatilloElegido)
        {

            PlatilloIngredientesMostrar elPlatillo = new();

            List<MenuIngredienteMostrar> laListaDeIngredientesParaMostrar = new();
            List<MenuIngredienteMostrar> laListaDeIngredientesFiltrada = new();

            List<MenuIngrediente> losIngredientesDelMenu = ObtengaLaListaDeIngredientesDelMenu();
            MenuIngredienteMostrar elIngredientePorDetallar;

            foreach (var item in losIngredientesDelMenu)
            {
                elIngredientePorDetallar = new();

                elIngredientePorDetallar.Id = item.Id;
                elIngredientePorDetallar.Id_Menu = item.Id_Menu;
                elIngredientePorDetallar.Id_Ingredientes = item.Id_Ingredientes;
                elIngredientePorDetallar.Cantidad = item.Cantidad;
                elIngredientePorDetallar.Id_Medidas = item.Id_Medidas;
                elIngredientePorDetallar.ValorAproximado = item.ValorAproximado;

                elIngredientePorDetallar.NombreIngrediente = ObtenerIngredientePorId(item.Id_Ingredientes).Nombre;
                elIngredientePorDetallar.NombreMedida = ObtenerMedidaPorId(item.Id_Medidas).Nombre;

                laListaDeIngredientesParaMostrar.Add(elIngredientePorDetallar);

            }

            laListaDeIngredientesFiltrada = laListaDeIngredientesParaMostrar.Where(x => x.Id_Menu == idPlatilloElegido).ToList();
            elPlatillo.ListaDeIngredientes = laListaDeIngredientesFiltrada;

            return elPlatillo;

        }

        public void AsocieElIngrediente(Model.MenuIngredienteMostrar ingrediente) 
        {


        }

        public void RemuevaElIngrediente(int idIngrediante)
        {


        }






        //Mesas::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        //      -------Publicos------
        public List<Mesa> ObtengaLaListaDeMesas()
        {
            var resultado = from c in ElContextoBD.Mesas select c;
            List<Mesa> lasMesas = resultado.ToList();

            return lasMesas;
        }//Fin metodo

        public void AgregueLaMesa(Mesa laMesa)
        {
            bool existe = ExisteLaMesa(laMesa.Nombre);

            if (existe == false)
            {
                ElContextoBD.Mesas.Add(laMesa);
                ElContextoBD.SaveChanges();
            }//Fin if
        }//Fin metodo

        public void EditarMesa(Mesa laMesa)
        {
            Model.Mesa laMesaAModificar = new Mesa();

            laMesaAModificar = ObtenerMesaPorId(laMesa.Id);

            laMesaAModificar.Nombre = laMesa.Nombre;
            laMesaAModificar.Estado = laMesa.Estado;

            ElContextoBD.Mesas.Update(laMesaAModificar);
            ElContextoBD.SaveChanges();
        }//Fin metodo

        public Mesa ObtengaElDetalleDeMesa(int id)
        {
            Mesa laMesa = ElContextoBD.Mesas.Find(id);
            return laMesa;
        }//Fin metodo

        public Mesa ObtenerMesaPorId(int id)
        {
            Model.Mesa laMesa;

            laMesa = ElContextoBD.Mesas.Find(id);

            return laMesa;
        }//Fin metodo

        //      -------Privados------
        private bool ExisteLaMesa(string nombre)
        {
            bool existe; existe = false;
            List<Mesa> lasMesas = ObtengaLaListaDeMesas();

            foreach (Mesa estaMesa in lasMesas)
            {
                if (estaMesa.Nombre.Equals(nombre))
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }//Fin metodo

        public void HabiliteLaMesa(Model.Mesa laMesa)
        {
            if (laMesa.Estado == Estado.NoDisponible)
            {
                laMesa.Estado = Model.Estado.Disponible;
            }//Fin if
            ElContextoBD.Mesas.Update(laMesa);
            ElContextoBD.SaveChanges();

        }//Fin metodo

        public void DeshabiliteLaMesa(Model.Mesa laMesa)
        {
            if (laMesa.Estado == Estado.Disponible)
            {
                laMesa.Estado = Model.Estado.NoDisponible;
            }

            ElContextoBD.Mesas.Update(laMesa);
            ElContextoBD.SaveChanges();

        }//Fin metodo





    }// FIN CLASE
}

