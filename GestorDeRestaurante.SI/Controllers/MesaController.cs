using Microsoft.AspNetCore.Mvc;

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        private readonly BS.IRepositorioRestaurante ElRepositorio;

        public MesaController(BS.IRepositorioRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }//Fin costructor



        // GET: api/<MesaController>
        [HttpGet("ObtengaLaListaDeMesas")]
        public IEnumerable<GestorDeRestaurante.Model.Mesa> ObtengaLaListaDeMesas()
        {
            List<Model.Mesa> lasMesas = ElRepositorio.ObtengaLaListaDeMesas();

            return lasMesas;

        }//Fin



        // GET: api/<MesaController>
        [HttpGet("ObtengaElDetalleDeMesa")]
        public GestorDeRestaurante.Model.Mesa ObtengaElDetalleDeMesa(string Id)
        {
            int id = int.Parse(Id);
            Model.Mesa elDetalleDeMesa = ElRepositorio.ObtengaElDetalleDeMesa(id);

            return elDetalleDeMesa;
            
        }//Fin 



        // POST api/<MesaController>
        [HttpPost("AgregueLaMesa")]
        public IActionResult AgregueLaMesa([FromBody] GestorDeRestaurante.Model.Mesa laMesa)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueLaMesa(laMesa);
                return Ok(laMesa);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }//Fin 



        // PUT api/<MesaController>
        [HttpPut("EditarMesa")]
        public IActionResult EditarMesa([FromBody] GestorDeRestaurante.Model.Mesa laMesa)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarMesa(laMesa);

                return Ok(laMesa);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }//Fin 



        // GET: api/<MesaController>
        [HttpGet("ObtenerMesaPorId")]
        public GestorDeRestaurante.Model.Mesa ObtenerMesaPorId(int id)
        {
            Model.Mesa laMesa;
            laMesa = ElRepositorio.ObtenerMesaPorId(id);

            return laMesa;
        }//Fin 



        // PUT api/<MesaController>
        [HttpPut("Habilitela")]
        public IActionResult Habilitela([FromBody] GestorDeRestaurante.Model.Mesa laMesa)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.HabiliteLaMesa(laMesa);
                return Ok(laMesa);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }//Fin



        // PUT api/<MesaController>
        [HttpPut("Deshabilitela")]
        public IActionResult Deshabilitela([FromBody] GestorDeRestaurante.Model.Mesa laMesa)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.DeshabiliteLaMesa(laMesa);

                return Ok(laMesa);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }//Fin 



    }//Fin class
}
