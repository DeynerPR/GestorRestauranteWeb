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
        public void AgregueElIngrediente(Model.Ingrediente ingrediente);    //Agregue
        public void EditarIngrediente(Model.Ingrediente ingrediente);       //Editar
        public Model.DetalleDelIngrediente ObtengaElDetalleDelIngrediente(int idIngredienteElegido); //Detalle
        public Model.Ingrediente ObtenerIngredientePorId(int Id);           



        //Menu
        public List<Platillo> ObtengaLosPlatillosDelMenu();             //Lista
        public void AgregueElPlatillo(Platillo elPlatillo);             //Agregue
        public void EditarPlatillo(Platillo elPlatillo);                //Editar
        public Model.Platillo ObtengaElDetalleDelPlatillo(int id);      //Detalle
        public Model.Platillo ObtenerPlatilloPorId(int Id);
        public Model.MenuCompleto ObtenerMenuCompleto();
        


         //MenuIngredientes




            //Mesa (Mesas)



            //Orden (MesaOrden)

        }
}