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

        //      -------Publicos------
        
        public List<PlatilloIngredientesMostrar> ObtengaLosPlatillosAMostrar()
        {
            List<Platillo> losPlatillos = ObtengaLosPlatillosDelMenu();

            List<PlatilloIngredientesMostrar> losPlatillosAMostrar = new List<PlatilloIngredientesMostrar>();
            PlatilloIngredientesMostrar elPlatilloAMostrar;
            
            List<MenuIngrediente> losMenuIngredienteFiltrado;
            int laSumaDeAproximados = 0;

            List<int> idsAnalizados = new List<int>();
            foreach (var esteRegistro in losPlatillos)
            {
                if (!idsAnalizados.Contains(esteRegistro.Id)) {
                    idsAnalizados.Add(esteRegistro.Id);

                    losMenuIngredienteFiltrado = ObtengaLosRegistrosDeEsteMenuEnMenuIngredientes(esteRegistro.Id);

                    foreach (var esteR in losMenuIngredienteFiltrado)
                    {
                        laSumaDeAproximados += esteR.ValorAproximado;
                    }//Fin foreach

                    elPlatilloAMostrar = new PlatilloIngredientesMostrar();
                    elPlatilloAMostrar.Id = esteRegistro.Id;
                    elPlatilloAMostrar.Nombre = ElContextoBD.Menu.Find(esteRegistro.Id).Nombre;
                    elPlatilloAMostrar.Precio = ElContextoBD.Menu.Find(esteRegistro.Id).Precio;
                    elPlatilloAMostrar.Ganancia = elPlatilloAMostrar.Precio - laSumaDeAproximados;
                    laSumaDeAproximados = 0;
                    losPlatillosAMostrar.Add(elPlatilloAMostrar);
                }
            }//Fin foreach
           
            return losPlatillosAMostrar;
        }//Fin metodo

        public List<MenuIngrediente> ObtengaLaListaDeMenuIngrediente()
        {

            var resultado = from c in ElContextoBD.MenuIngredientes select c;

            return resultado.ToList();

        }

        public MenuIngredienteAsociar ObtengaElPlatilloConDatosAsociados(int id) {

            MenuIngredienteAsociar elPlatilloConAsociados = new MenuIngredienteAsociar();
            elPlatilloConAsociados.Id_menu = id;
            elPlatilloConAsociados.laListaDeIngredientes = ObtengaNombresDeLosIngredientesAsociadosAlPlatillo(id);

            return elPlatilloConAsociados;
        }//Fin metodo

        public MenuIngredienteAsociar ObtengaElPlatilloConDatosAAsociar(int id) { 
            
            MenuIngredienteAsociar elPlatilloParaAsociar = new MenuIngredienteAsociar();
            elPlatilloParaAsociar.Id_menu = id;
            elPlatilloParaAsociar.laListaDeIngredientes = ObtengaNombresDeLosIngredientesDesasociadosAlPlatillo(id);
            elPlatilloParaAsociar.laListaDeMedidas      = ObtengaNombresDeLasMedidas();

            return elPlatilloParaAsociar;
        }//Fin metodo

        public void AsocieAlPlatillo(Model.MenuIngredienteAsociar losDatos)
        {

            MenuIngrediente elMenuIngrediente = new MenuIngrediente();

            elMenuIngrediente.Id_Menu = losDatos.Id_menu;
            elMenuIngrediente.Id_Ingredientes = ObtengaIdIngredientePorNombre(losDatos.elIngredienteSeleccionado);
            elMenuIngrediente.Id_Medidas = ObtengaIdMedidaPorNombre(losDatos.laMedidaSeleccionada);
            elMenuIngrediente.Cantidad = losDatos.Cantidad;
            elMenuIngrediente.ValorAproximado = losDatos.ValorAproximado;

            ElContextoBD.MenuIngredientes.Add(elMenuIngrediente);
            ElContextoBD.SaveChanges();

        }//Fin metodo


        public void DesasocieAlPlatillo(Model.MenuIngredienteAsociar elMenuIngrediente) {

            int idMenuElegido = elMenuIngrediente.Id_menu;
            string nombreIngredienteElegido = elMenuIngrediente.laListaDeIngredientes[0];

            string elNombreDelIngrediente;
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientes();
            List<MenuIngrediente> losMenuIngrediente = ObtengaLaListaDeMenuIngrediente();

            MenuIngrediente elMenu = new MenuIngrediente();
            foreach (var esteRegistro in losMenuIngrediente)
            {
                elNombreDelIngrediente = ObtengaElNombreDelIngrediente(esteRegistro.Id_Ingredientes, losIngredientes);
                if (esteRegistro.Id_Menu == idMenuElegido && elNombreDelIngrediente == nombreIngredienteElegido) {

                    elMenu = ElContextoBD.MenuIngredientes.Find(esteRegistro.Id);
                    ElContextoBD.MenuIngredientes.Remove(elMenu);
                    ElContextoBD.SaveChanges();
                    break;
                }
            }
        }//Fin metodo


        //      -------Privados------

        private List<string> ObtengaNombresDeLosIngredientes()
        {
            List<string> losNombres = new List<string>();
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientes();

            foreach (var esteIngrediente in losIngredientes)
            {
                losNombres.Add(esteIngrediente.Nombre);
            }//Fin foreach

            return losNombres;
        }//Fin metodo

        private List<string> ObtengaNombresDeLasMedidas()
        {
            List<string> losNombres = new List<string>();
            List<Medida> lasMedidas = ObtengaLaListaDeMedidas();

            foreach (var estaMedida in lasMedidas)
            {
                losNombres.Add(estaMedida.Nombre);
            }//Fin foreach

            return losNombres;
        }//Fin metodo

        private List<string> ObtengaNombresDeLosIngredientesAsociadosAlPlatillo(int id)
        {
            List<Ingrediente> losIngredientes = ObtengaLaListaDeIngredientes();
            List<MenuIngrediente> losRegistros = ObtengaLosRegistrosDeEsteMenuEnMenuIngredientes(id);


            List<string> losAsociados = new List<string>();
            
            string elNombre;
            foreach (var esteRegistro in losRegistros)
            {
                elNombre = ObtengaElNombreDelIngrediente(esteRegistro.Id_Ingredientes, losIngredientes);
                losAsociados.Add(elNombre);
            }//Fin metodo


            return losAsociados;
        }//Fin metodo

        private List<string> ObtengaNombresDeLosIngredientesDesasociadosAlPlatillo(int id) {

            List<string> Asociados = ObtengaNombresDeLosIngredientesAsociadosAlPlatillo(id);
            List<string> Desasociados = ObtengaNombresDeLosIngredientes();

            foreach (var esteAsociado in Asociados) {
                Desasociados.Remove(esteAsociado);
            }

            return Desasociados;
        }//Fin metodo

        private List<MenuIngrediente> ObtengaLosRegistrosDeEsteMenuEnMenuIngredientes(int id)
        {
            var resultado = from c in ElContextoBD.MenuIngredientes select c;
            List<MenuIngrediente> losRegistros = resultado.Where(x => x.Id_Menu == id).ToList();

            return losRegistros;
        }//Fin metodo

        private int ObtengaIdMedidaPorNombre(string laMedidaSeleccionada)
        {
            List<Medida> medidas = ObtengaLaListaDeMedidas();
            int laId = 0;
            foreach (var estaMedida in medidas)
            {
                if (estaMedida.Nombre == laMedidaSeleccionada)
                {
                    laId = estaMedida.Id;
                    break;
                }
            }
            return laId;
        }//Fin metodo

        private int ObtengaIdIngredientePorNombre(string elIngredienteSeleccionado)
        {
            List<Ingrediente> ingredientes = ObtengaLaListaDeIngredientes();
            int laId = 0;
            foreach (var esteIngrediente in ingredientes)
            {
                if (esteIngrediente.Nombre == elIngredienteSeleccionado) {
                    laId = esteIngrediente.Id;
                    break;
                }
            }
            return laId;
        }//Fin metodo






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

