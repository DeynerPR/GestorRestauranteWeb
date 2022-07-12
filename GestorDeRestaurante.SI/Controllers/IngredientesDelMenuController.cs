using Microsoft.AspNetCore.Mvc;

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesDelMenuController : ControllerBase
    {

        private readonly BS.IRepositorioRestaurante ElRepositorio;

        public IngredientesDelMenuController(BS.IRepositorioRestaurante repositorio)
        {

            ElRepositorio = repositorio;

        }


        // GET api/<IngredientesDelMenuController>
        [HttpGet("ObtengaLosPlatillos")]
        public IEnumerable<GestorDeRestaurante.Model.PlatilloIngredientesMostrar> ObtengaLosPlatillos()
        {
            List<Model.PlatilloIngredientesMostrar> losPlatillosPorMostrar = ElRepositorio.ObtengaLosPlatillosAMostrar();

            return losPlatillosPorMostrar;
        }


        // GET api/<IngredientesDelMenuController>
        [HttpGet("ObtengaElPlatilloConDatosAAsociar")]
        public GestorDeRestaurante.Model.MenuIngredienteAsociar ObtengaElPlatilloConDatosAAsociar(string Id)
        {

            int id = int.Parse(Id);
            Model.MenuIngredienteAsociar elPlatillo = ElRepositorio.ObtengaElPlatilloConDatosAAsociar(id);

            return elPlatillo;

        }
        
       
            // GET api/<IngredientesDelMenuController>
        [HttpGet("ObtengaElPlatilloConDatosAsociados")]
        public GestorDeRestaurante.Model.MenuIngredienteAsociar ObtengaElPlatilloConDatosAsociados(string Id)
        {

            int id = int.Parse(Id);
            Model.MenuIngredienteAsociar elPlatillo = ElRepositorio.ObtengaElPlatilloConDatosAsociados(id);

            return elPlatillo;

        }




        // POST api/<IngredientesDelMenuController>
        [HttpPost("Asociar")]
        public IActionResult Asociar([FromBody] GestorDeRestaurante.Model.MenuIngredienteAsociar elMenuIngrediente)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AsocieAlPlatillo(elMenuIngrediente);

                return Ok(elMenuIngrediente);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpPost("Desasociar")]
        public IActionResult Desasociar([FromBody] GestorDeRestaurante.Model.MenuIngredienteAsociar elMenuIngrediente)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.DesasocieAlPlatillo(elMenuIngrediente);

                return Ok(elMenuIngrediente);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
