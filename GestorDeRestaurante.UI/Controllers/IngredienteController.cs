using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class IngredienteController : Controller
    {


        // GET: IngredientesController
        public async Task<IActionResult> Index()
        {

            List<Model.Ingrediente> losPlatillosDetallados;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://localhost:7181/api/Ingrediente/ObtengaLosIngredientes");
                string apiResponse = await response.Content.ReadAsStringAsync();
                losPlatillosDetallados = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Ingrediente>>(apiResponse);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(losPlatillosDetallados);
        }


        
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Model.Ingrediente elIngrediente)
        {
            try
            {
                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elIngrediente);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7181/api/Ingrediente/", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }//Fin metodo



        





    }//Fin clase

}
