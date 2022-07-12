using Microsoft.AspNetCore.Mvc;

namespace GestorDeRestaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaController : ControllerBase
    {

        private readonly BS.IRepositorioRestaurante ElRepositorio;

        public MedidaController(BS.IRepositorioRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }



        // GET: api/<MedidaController>
        [HttpGet("ObtengaLasMedidas")]
        public IEnumerable<GestorDeRestaurante.Model.Medida> ObtengaLasMedidas()
        {
            List<Model.Medida> lasMedidas = ElRepositorio.ObtengaLaListaDeMedidas();
            
            return lasMedidas;
        }



        // POST api/<MedidaController>
        [HttpPost("AgregueLaMedida")]
        public IActionResult AgregueLaMedida([FromBody] GestorDeRestaurante.Model.Medida medida)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueLaMedida(medida);

                return Ok(medida);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        // GET: api/<MedidaController>
        [HttpGet("ObtenerMedidaPorId")]
        public GestorDeRestaurante.Model.Medida ObtenerMedidaPorId(int id)
        {
            Model.Medida laMedida;
            laMedida = ElRepositorio.ObtenerMedidaPorId(id);

            return laMedida;
        }



        // PUT api/<MedidaController>
        [HttpPut("EditarMedida")]
        public IActionResult EditarMedida([FromBody] GestorDeRestaurante.Model.Medida laMedida)
        {

            if (ModelState.IsValid)
            {
                ElRepositorio.EditarMedida(laMedida);

                return Ok(laMedida);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



    }
}
