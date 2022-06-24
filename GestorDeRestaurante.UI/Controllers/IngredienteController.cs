using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestorDeRestaurante.UI.Controllers
{
    public class IngredienteController : Controller
    {




        private HttpClient httpClient;





        // GET: IngredientesController
        public async Task<IActionResult> Index()
        {
            try
            {

                List<Model.Ingrediente> losPlatillosDetallados;

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7078/api/Ingredientes/ObtengaLosIngredientes");
                string apiResponse = await response.Content.ReadAsStringAsync();
                losPlatillosDetallados = JsonConvert.DeserializeObject<List<Model.Ingrediente>>(apiResponse);

            }
            catch (Exception)
            {

                throw;
            }








            return View();
        }


        /*
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async ActionResult Agregar(Model.Ingrediente elIngrediente)
        {
            try
            {
               



                if (SI.Controllers.IngredientesController. == false) {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Agregar));
                }
            }
            catch
            {
                return View();
            }
        }//Fin metodo



        */





    }//Fin clase

}
