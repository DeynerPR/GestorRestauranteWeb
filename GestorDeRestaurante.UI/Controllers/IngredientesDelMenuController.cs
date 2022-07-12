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








        [HttpGet]
        public async Task<IActionResult> Asociar(int Id)
        {
            Model.MenuIngredienteAsociar elPlatilloConDatosAsocioar;

            try
            {

                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/IngredientesDelMenu/ObtengaElPlatilloConDatosAAsociar", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elPlatilloConDatosAsocioar = JsonConvert.DeserializeObject<Model.MenuIngredienteAsociar>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (elPlatilloConDatosAsocioar.laListaDeIngredientes.Count != 0)
            {
                return View(elPlatilloConDatosAsocioar);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }//Fin metodo


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsociarPost(Model.MenuIngredienteAsociar elPlatillo)
        {
            try
            {
                if (elPlatillo.laListaDeIngredientes != null)
                {
                    elPlatillo.elIngredienteSeleccionado = elPlatillo.laListaDeIngredientes[0];
                    elPlatillo.laMedidaSeleccionada = elPlatillo.laListaDeMedidas[0];

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(elPlatillo);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://localhost:7181/api/IngredientesDelMenu/Asociar", byteContent);
                }
            }
            catch
            {
                return View();
            }

            if (elPlatillo.laListaDeIngredientes != null)
            {
                return RedirectToAction(nameof(Asociar), new { id = elPlatillo.Id_menu });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }//Fin metodo



        public async Task<IActionResult> Desasociar(int Id)
        {

            Model.MenuIngredienteAsociar elPlatilloConDatosAsociados;
            try
            {

                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/IngredientesDelMenu/ObtengaElPlatilloConDatosAsociados", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elPlatilloConDatosAsociados = JsonConvert.DeserializeObject<Model.MenuIngredienteAsociar>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (elPlatilloConDatosAsociados.laListaDeIngredientes.Count > 0)
            {
                return View(elPlatilloConDatosAsociados);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }//Fin metodo


        [HttpPost]
        public async Task<IActionResult> DesasociarPost(Model.MenuIngredienteAsociar elPlatillo)
        {
            try
            {
                if (elPlatillo.laListaDeIngredientes != null)
                {
                    elPlatillo.elIngredienteSeleccionado = elPlatillo.laListaDeIngredientes[0];

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(elPlatillo);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://localhost:7181/api/IngredientesDelMenu/Desasociar", byteContent);
                }
            }
            catch
            {
                return View();
            }

            if (elPlatillo.laListaDeIngredientes != null)
            {
                return RedirectToAction(nameof(Desasociar), new { id = elPlatillo.Id_menu });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }//Fin class


    }//Fin clase
}
