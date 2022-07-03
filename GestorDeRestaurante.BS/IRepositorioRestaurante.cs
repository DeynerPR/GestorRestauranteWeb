using GestorDeRestaurante.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.BS
{
    public interface IRepositorioRestaurante
    {

        //Medidas
        public List<Model.Medida> ObtengaLaListaDeMedidas();  //Lista
        public void AgregueLaMedida(Model.Medida medida);     //Agregue
        public void EditarMedida(Model.Medida medida);        //Editar
        public Model.Medida ObtenerMedidaPorId(int Id);       //Detalle

        

        //Ingredientes
        public List<Model.Ingrediente> ObtengaLaListaDeIngredientes();      //Lista
        List<Mesa> ObtengaLaListaDeMesas();
        public void AgregueElIngrediente(Model.Ingrediente ingrediente);    //Agregue
        public void EditarIngrediente(Model.Ingrediente ingrediente);       //Editar
        public Model.DetalleDelIngrediente ObtengaElDetalleDelIngrediente(int idIngredienteElegido); //Detalle
        Mesa ObtengaElDetalleDeMesa(int id);
        public Model.Ingrediente ObtenerIngredientePorId(int Id);



        //MenuIngredientes
        public List<PlatilloIngredientesMostrar> ConviertaLosPlatillosParaMostrar(List<Platillo> losPlatillos);
        public PlatilloIngredientesMostrar ConviertaElPlatilloParaMostrar(Platillo elPlatillo);
        public List<MenuIngrediente> ObtengaLaListaDeIngredientesDelMenu();
        public PlatilloIngredientesMostrar ObtengaLaListaDeIngredientesDelPlatillo(int id);
        public void AsocieElIngrediente(Model.MenuIngredienteMostrar ingrediente);
        public void RemuevaElIngrediente(int idIngrediante);


        //Menu
        public List<Platillo> ObtengaLosPlatillosDelMenu();             //Lista
        public void AgregueElPlatillo(Platillo elPlatillo);             //Agregue
        public void EditarPlatillo(Platillo elPlatillo);                //Editar
        void AgregueLaMesa(Mesa laMesa);
        public Model.Platillo ObtengaElDetalleDelPlatillo(int id);      //Detalle
        public Model.Platillo ObtenerPlatilloPorId(int Id);
        public Model.MenuCompleto ObtenerMenuCompleto();
        Mesa ObtenerMesaPorId(int id);
        void EditarMesa(Mesa laMesa);
        void DeshabiliteLaMesa(Mesa laMesa);
        void HabiliteLaMesa(Mesa laMesa);



        //Mesa (Mesas)



        //Orden (MesaOrden)

    }
}