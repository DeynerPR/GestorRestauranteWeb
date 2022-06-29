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
        //Medida (Medidas)
        public List<Model.Medida> ObtengaLaListaDeMedidas();
        public bool ExisteLaMedida(string nombre);
        void AgregueLaMedida(Model.Medida medida);
        Model.Medida ObtenerMedidaPorId(int Id);
        void EditarMedida(Model.Medida medida);
        public string ObtengaElNombreDeLaMedida(int idDMedidaEnPlatillo, List<Model.Medida> lasMedidas);


        //Ingrediente (Ingredientes)
        public List<Model.Ingrediente> ObtengaLaListaDeIngredientes();
        public bool ExisteElIngrediente(string nombre);
        public void AgregueElIngrediente(Model.Ingrediente ingrediente);
        Model.Ingrediente ObtenerIngredientePorId(int Id);
        void EditarIngrediente(Model.Ingrediente ingrediente);
        public Model.DetalleDelIngrediente ObtengaElDetalleDelIngrediente(int idIngredienteElegido);


        //Platillo (Menu)
        void AgregueElPlatillo(Platillo elPlatillo);



        //PlatilloIngredientes (MenuIngredientes)



        //Mesa (Mesas)



        //Orden (MesaOrden)

    }
}