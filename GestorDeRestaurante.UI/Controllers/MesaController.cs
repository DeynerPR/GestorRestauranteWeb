using GestorDeRestaurante.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class MesaController : Controller
    {


        // GET: MesaController
        public async Task<IActionResult> Index()
        {

            List<Model.Mesa> lasMesas;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://localhost:7181/api/Mesa/ObtengaLaListaDeMesas");
                string apiResponse = await response.Content.ReadAsStringAsync();
                lasMesas = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Mesa>>(apiResponse);
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return View(DemeLasMesasConvertidas(lasMesas));
        }//Fin 

        private List<Models.Mesa> DemeLasMesasConvertidas(List<Model.Mesa> lasMesas)
        {
            List<Models.Mesa> lasMesasData = new List<Models.Mesa>();
            Models.Mesa laMesaData;
            foreach (var estaMesa in lasMesas)
            {
                laMesaData = new Models.Mesa();
                laMesaData.Id = estaMesa.Id;
                laMesaData.Nombre = estaMesa.Nombre;
                laMesaData.Estado = (Models.Estado)estaMesa.Estado;
                lasMesasData.Add(laMesaData);

            }//Fin foreach
            return lasMesasData;
        }//Fin metodo





        // GET: MesaController/Agregar
        public ActionResult Agregar()
        {
            Models.Mesa laMesaData = new Models.Mesa();
            laMesaData.Estado = Models.Estado.Disponible;
            return View(laMesaData);
        }//Fin


        // POST: MesaController/Agregar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Models.Mesa laMesaData)
        {
            try
            {
                Model.Mesa laMesa = new Model.Mesa();
                laMesa.Nombre = laMesaData.Nombre;
                laMesa.Estado = Model.Estado.Disponible;

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(laMesa);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7181/api/Mesa/AgregueLaMesa", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }//Fin 



        // GET: MesaController/Editar
        public async Task<IActionResult> Editar(int Id)
        {

            Model.Mesa laMesa;
            Models.Mesa laMesaAData = new Models.Mesa();

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Mesa/ObtenerMesaPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                laMesa = JsonConvert.DeserializeObject<Model.Mesa>(apiResponse);
                laMesaAData.Id = laMesa.Id;
                laMesaAData.Nombre = laMesa.Nombre;
                laMesaAData.Estado = (Models.Estado)laMesa.Estado;

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMesaAData);
        }//Fin 


        // POST: MesaController/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Models.Mesa laMesaData)
        {
            try
            {
                Model.Mesa laMesa = new Model.Mesa();

                laMesa.Id = laMesaData.Id;
                laMesa.Nombre = laMesaData.Nombre;
                laMesa.Estado = (Estado)laMesaData.Estado;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(laMesa);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7181/api/Mesa/EditarMesa", byteContent);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();

            }
        }//Fin 


        // GET: MesaController/Detalle
        public async Task<IActionResult> Detalle(int Id)
        {
            Models.Mesa laMesaData = new Models.Mesa();
            Model.Mesa laMesa;

            try
            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Mesa/ObtenerMesaPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMesa = JsonConvert.DeserializeObject<Model.Mesa>(apiResponse);

                laMesaData.Id = laMesa.Id;
                laMesaData.Nombre = laMesa.Nombre;
                laMesaData.Estado = (Models.Estado)laMesa.Estado;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMesaData);
        }//Fin 



        // GET: MesaController/Habilitela
        public async Task<IActionResult> HabiliteLaMesa(int Id)
        {

            Model.Mesa laMesa;

            try
            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString(),
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Mesa/ObtenerMesaPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMesa = JsonConvert.DeserializeObject<Model.Mesa>(apiResponse);


                httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(laMesa);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await httpClient.PutAsync("https://localhost:7181/api/Mesa/Habilitela", byteContent);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(Index));

        }//Fin 



        // GET: MesaController/Deshabilitela
        public async Task<IActionResult> DeshabilitLaMesa(int Id)
        {

            Model.Mesa laMesa;

            try
            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString(),
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Mesa/ObtenerMesaPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMesa = JsonConvert.DeserializeObject<Model.Mesa>(apiResponse);


                httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(laMesa);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await httpClient.PutAsync("https://localhost:7181/api/Mesa/Deshabilitela", byteContent);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(Index));

        }//Fin 


    }//Fin class






}
