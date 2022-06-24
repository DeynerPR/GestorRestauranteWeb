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



        // GET: api/<IngredientesController>
        [HttpGet("ObtengaLosIngredientes")]
        public IEnumerable<GestorDeRestaurante.Model.Ingrediente> ObtengaLosIngredientes()
        {
            List<Model.Ingrediente> losIngredientes = ElRepositorio.ObtengaLaListaDeIngredientesGeneral();
            return losIngredientes;
        }//Fin metodo


        [HttpPost]
        public IActionResult Post([FromBody] GestorDeRestaurante.Model.Ingrediente elIngrediente)
        {
            if (ModelState.IsValid) {
                ElRepositorio.AgregarNuevoIngrediente(elIngrediente);
                return Ok(elIngrediente);
            }
            else {
                return BadRequest(ModelState);
            }
        }//Fin metodo
    }
}
