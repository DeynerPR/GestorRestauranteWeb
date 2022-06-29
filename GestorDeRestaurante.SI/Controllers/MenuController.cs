using Microsoft.AspNetCore.Mvc;

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly BS.IRepositorioRestaurante ElRepositorio;

        public MenuController(BS.IRepositorioRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }



        [HttpPost("AgregueElPlatillo")]
        public IActionResult AgregueElPlatillo([FromBody] GestorDeRestaurante.Model.Platillo elPlatillo)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueElPlatillo(elPlatillo);
                return Ok(elPlatillo);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }









       




    }//Fin class
}
