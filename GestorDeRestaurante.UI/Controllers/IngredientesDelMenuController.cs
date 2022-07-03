using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class IngredientesDelMenuController : Controller
    {

        //GET: IngredienteController
        public async Task<IActionResult> Index()
        {

            List<Model.PlatilloIngredientesMostrar> losPlatillosRecibidos;

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7181/api/IngredientesDelMenu/ObtengaLosPlatillos");
                string apiResponse = await response.Content.ReadAsStringAsync();

                losPlatillosRecibidos = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.PlatilloIngredientesMostrar>>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(losPlatillosRecibidos);
        }



        public async Task<IActionResult> Ingredientes(int Id)
        {
            Model.PlatilloIngredientesMostrar elPlatillo;

            try
            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/IngredientesDelMenu/ObtengaLosIngredientesDelPlatillo", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elPlatillo = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.PlatilloIngredientesMostrar>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(elPlatillo);

        }



        public ActionResult Asociar()
        {
            
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Asociar(Models.MenuIngredienteAsociar ingrediente)
        {
            try
            {
                Model.MenuIngredienteMostrar elIngrediente = new();

                elIngrediente.Id = ingrediente.Id;
               // elIngrediente.Id_Menu = 
                elIngrediente.NombreIngrediente = ingrediente.NombreIngrediente;
                elIngrediente.NombreMedida = ingrediente.NombreMedida;
                elIngrediente.Cantidad = ingrediente.Cantidad;
                elIngrediente.ValorAproximado = ingrediente.ValorAproximado;

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elIngrediente);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7181/api/IngredientesDelMenuController/AsociarIngrediente", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        public async Task<IActionResult> Desasociar(int id)
        {


            return RedirectToAction(nameof(Ingredientes));
        }




    }//Fin class
}
