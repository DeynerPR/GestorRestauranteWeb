using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {


        private readonly BS.IRepositorioRestaurante ElRepositorio;

        public IngredienteController(BS.IRepositorioRestaurante elRepo)
        {
            ElRepositorio = elRepo;
        }



        // POST api/<IngredientesController>
        [HttpPost()]
        public void Agregar([FromBody] string value)
        {




        }//Fin post



        // GET: api/<IngredientesController>
        [HttpGet("ObtengaLosIngredientes")]
        public IEnumerable<Model.Ingrediente> ObtengaLosIngredientes()
        {
            List<Model.Ingrediente> losIngredientes = ElRepositorio.ObtengaLaListaDeIngredientesGeneral();
            return losIngredientes;
        }//Fin metodo



        [HttpPost]
        public bool AgregarNuevoIngrediente(Model.Ingrediente elIngrediente)
        {
            bool resultado = ElRepositorio.AgregarNuevoIngrediente(elIngrediente);
            return resultado;
        }//Fin metodo
    }
}
