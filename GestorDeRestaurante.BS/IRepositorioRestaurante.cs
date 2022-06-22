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
        List<Model.Medida> ObtengaLaListaDeMedidas();
        void AgregueLaMedida(Model.Medida medida);
        Model.Medida ObtenerMedidaPorId(int Id);
        void EditarMedida(Model.Medida medida);
        Model.Medida ObtengaLaMedida(int Id);

        //Ingrediente (Ingredientes)


        //Platillo (Menu)


        //PlatilloIngredientes (MenuIngredientes)


        //Mesa (Mesas)


        //Orden (MesaOrden)

    }
}
