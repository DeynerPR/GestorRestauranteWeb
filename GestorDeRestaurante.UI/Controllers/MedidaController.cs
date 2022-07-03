using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class MedidaController : Controller
    {


        //GET: MedidaController
        public async Task<IActionResult> Index()
        {
            List<Model.Medida> laListaDeMedidas;

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7181/api/Medida/ObtengaLasMedidas");
                
                string apiResponse = await response.Content.ReadAsStringAsync();
                
                laListaDeMedidas = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Medida>>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(laListaDeMedidas);

        }//Fin 



        // GET: MedidaController/Agregar
        public ActionResult Agregar()
        {
            return View();
        }//Fin 


        // POST: MedidaController/Agregar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Models.Medida medida)
        {
            try
            {
                Model.Medida laMedida = new Model.Medida();

                laMedida.Id = medida.Id;
                laMedida.Nombre = medida.Nombre;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(laMedida);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7181/api/Medida/AgregueLaMedida", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }//Fin 



        // GET: MedidaController/Editar
        public async Task <IActionResult> Editar(int Id)
        {

            Model.Medida laMedida;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Medida/ObtenerMedidaPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                laMedida = JsonConvert.DeserializeObject<Model.Medida>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMedida);
        }//Fin 


        // POST: MedidaController/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Model.Medida medida)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(medida);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7181/api/Medida/EditarMedida", byteContent);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();

            }
        }//Fin 



        // GET: MedidaController/Detalle
        public async Task<IActionResult> Detalle(int Id)
        {
            Model.Medida laMedida;

            try
            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Medida/ObtenerMedidaPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMedida = JsonConvert.DeserializeObject<Model.Medida>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMedida);
        }//Fin 




    }//Fin class
}
