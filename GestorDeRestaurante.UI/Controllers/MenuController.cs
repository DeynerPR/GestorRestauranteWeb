using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class MenuController : Controller
    {



        // GET: MenuController
        public async Task<IActionResult> Index()
        {

            //List<Models.Platillo> losPlatillosParaMostrar;
            List<Model.Platillo> losPlatillosRecibidos;

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7181/api/Menu/ObtengaLosPlatillos");
                string apiResponse = await response.Content.ReadAsStringAsync();
                losPlatillosRecibidos = JsonConvert.DeserializeObject<List<GestorDeRestaurante.Model.Platillo>>(apiResponse);

                //losPlatillosParaMostrar = ConvertirPlatillosParaMostrar(losPlatillosRecibidos);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(losPlatillosRecibidos);
        }//Fin 


        //POST: MenuController
        public ActionResult Agregar()
        {
            return View();
        }//Fin


        //POST: MenuController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Models.Platillo PlatilloData)
        {
            try
            {
                GestorDeRestaurante.Model.Platillo elPlatillo = new Model.Platillo();

                elPlatillo.Nombre = PlatilloData.Nombre;
                elPlatillo.Categoria = (Model.Categoria)PlatilloData.Categoria;
                elPlatillo.Precio = PlatilloData.Precio;

                if (PlatilloData.Imagen.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        PlatilloData.Imagen.CopyTo(ms);
                        elPlatillo.Imagen = ms.ToArray();
                    }
                }


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elPlatillo);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7181/api/Menu/AgregueElPlatillo", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch 
            {

                return View();
            }

        }//Fin 


        //GET: MenuController
        public async Task<IActionResult> Detalle(int Id)
        {
            Model.Platillo elDatelleDelPlatillo;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Menu/ObtengaElDetalleDelPlatillo", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                elDatelleDelPlatillo = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Platillo>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(elDatelleDelPlatillo);

        }//Fin metodo




        //GET: MenuController
        public async Task<IActionResult> Editar(int Id)
        {

            Model.Platillo elPlatillo;
            Models.Platillo elPlatilloAMostrar;

            try
            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Menu/ObtenerPlatilloPorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elPlatillo = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Platillo>(apiResponse);

                elPlatilloAMostrar = new Models.Platillo();
                elPlatilloAMostrar.Id = elPlatillo.Id; 
                elPlatilloAMostrar.Nombre = elPlatillo.Nombre;
                elPlatilloAMostrar.Categoria = (Models.Categoria)elPlatillo.Categoria;
                elPlatilloAMostrar.Precio = elPlatillo.Precio;
                elPlatilloAMostrar.Imagen = ConvertirLosBytes(elPlatillo.Imagen);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(elPlatilloAMostrar);

        }//Fin 


        //POST: MenuController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Models.Platillo PlatilloData)
        {
            GestorDeRestaurante.Model.Platillo elPlatillo;


            try
            {
                elPlatillo = new Model.Platillo();
                elPlatillo.Id = PlatilloData.Id;
                elPlatillo.Nombre = PlatilloData.Nombre;
                elPlatillo.Categoria = (Model.Categoria)PlatilloData.Categoria;
                elPlatillo.Precio = PlatilloData.Precio;

                if (PlatilloData.Imagen.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        PlatilloData.Imagen.CopyTo(ms);
                        elPlatillo.Imagen = ms.ToArray();
                    }
                }


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elPlatillo);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7181/api/Menu/EditarPlatillo", byteContent);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }//Fin 


        private IFormFile ConvertirLosBytes(byte[] imagen)
        {
            var stream = new MemoryStream(imagen);
            IFormFile file = new FormFile(stream, 0, imagen.Length, "Imagen", "Name");
            return file;
        }//Fin metodo



        public async Task<IActionResult> MenuCompleto()
        {
            Model.MenuCompleto elMenuCompleto;

            try

            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://localhost:7181/api/Menu/ObtenerMenuCompleto");
                string apiResponse = await response.Content.ReadAsStringAsync();
                elMenuCompleto = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.MenuCompleto>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(elMenuCompleto);

        }//Fin metodo






    }//Fin clase
}
