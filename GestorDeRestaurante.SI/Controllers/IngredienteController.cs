using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {

        private readonly BS.IRepositorioRestaurante ElRepositorio;

        public IngredienteController(BS.IRepositorioRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }



        // GET: api/<IngredienteController>
        [HttpGet("ObtengaLosIngredientes")]
        public IEnumerable<GestorDeRestaurante.Model.Ingrediente> ObtengaLosIngredientes()
        {
            List<Model.Ingrediente> losIngredientes = ElRepositorio.ObtengaLaListaDeIngredientes();
            return losIngredientes;
        }

        // POST api/<MedidaController>
        [HttpPost("AgregueElIngrediente")]
        public IActionResult AgregueElIngrediente([FromBody] GestorDeRestaurante.Model.Ingrediente elIngrediente)
        {
            if (ModelState.IsValid) {
                ElRepositorio.AgregueElIngrediente(elIngrediente);
                return Ok(elIngrediente);
            }
            else {
                return BadRequest(ModelState);
            }
        }

        // GET: api/<IngredienteController>
        [HttpGet("ObtenerIngredientePorId")]
        public GestorDeRestaurante.Model.Ingrediente ObtenerIngredientePorId(int id)
        {
            Model.Ingrediente elIngrediente;
            elIngrediente = ElRepositorio.ObtenerIngredientePorId(id);

            return elIngrediente;
        }

        // PUT api/<IngredienteController>
        [HttpPut("EditarIngrediente")]
        public IActionResult EditarIngrediente([FromBody] GestorDeRestaurante.Model.Ingrediente ingrediente)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.EditarIngrediente(ingrediente);

                return Ok(ingrediente);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
