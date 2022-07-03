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
            List<Model.Platillo> losPlatillos = ElRepositorio.ObtengaLosPlatillosDelMenu();
            List<Model.PlatilloIngredientesMostrar> losPlatillosPorMostrar;

            losPlatillosPorMostrar = ElRepositorio.ConviertaLosPlatillosParaMostrar(losPlatillos);


            return losPlatillosPorMostrar;
        }



        // GET api/<IngredientesDelMenuController>
        [HttpGet("ObtengaLosIngredientesDelPlatillo")]
        public GestorDeRestaurante.Model.PlatilloIngredientesMostrar ObtengaLosIngredientesDelPlatillo(string Id)
        {

            int id = int.Parse(Id);
            Model.PlatilloIngredientesMostrar elPlatilloParaMostrar = ElRepositorio.ObtengaLaListaDeIngredientesDelPlatillo(id);

            return elPlatilloParaMostrar;

        }



        // POST api/<IngredientesDelMenuController>
        [HttpPost("AsociarIngrediente")]
        public IActionResult AsociarIngrediente([FromBody] GestorDeRestaurante.Model.MenuIngredienteMostrar ingrediente)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AsocieElIngrediente(ingrediente);

                return Ok(ingrediente);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpDelete("DesasociarIngrediente")]
        public IActionResult DesasociarIngrediente(string id)
        {
            int Id = int.Parse(id);

            ElRepositorio.RemuevaElIngrediente(Id);

            return Ok();

        }

    }
}
