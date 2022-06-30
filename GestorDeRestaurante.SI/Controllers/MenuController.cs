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
        }//Fin Constructor



        // POST api/<MenuController>
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
        }//Fin 



        // GET api/<MenuController>
        [HttpGet("ObtengaLosPlatillos")]
        public IEnumerable<GestorDeRestaurante.Model.Platillo> ObtengaLosPlatillos()
        {
            List<Model.Platillo> losPlatillos = ElRepositorio.ObtengaLosPlatillosDelMenu();
            return losPlatillos;
        }//Fin 


        // GET api/<MenuController>
        [HttpGet("ObtengaElDetalleDelPlatillo")]
        public GestorDeRestaurante.Model.Platillo ObtengaElDetalleDelPlatillo(string Id)
        {
            int id = int.Parse(Id);
            Model.Platillo elDetalleDelPlatillo = ElRepositorio.ObtengaElDetalleDelPlatillo(id);
            return elDetalleDelPlatillo;
        }//Fin 


        // PUT api/<MenuController>
        [HttpPut("EditarPlatillo")]
        public IActionResult EditarPlatillo([FromBody] GestorDeRestaurante.Model.Platillo elPlatillo)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.EditarPlatillo(elPlatillo);

                return Ok(elPlatillo);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }//Fin 


        // GET: api/<IngredienteController>
        [HttpGet("ObtenerPlatilloPorId")]
        public GestorDeRestaurante.Model.Platillo ObtenerPlatilloPorId(int id)
        {
            Model.Platillo elPlatillo;
            elPlatillo = ElRepositorio.ObtenerPlatilloPorId(id);

            return elPlatillo;
        }//Fin 


        // GET api/<MenuController>
        [HttpGet("ObtenerMenuCompleto")]
        public GestorDeRestaurante.Model.MenuCompleto ObtenerMenuCompleto()
        {
            Model.MenuCompleto elMenuCompleto = ElRepositorio.ObtenerMenuCompleto();
            return elMenuCompleto;
        }//Fin 


    }//Fin class
}
