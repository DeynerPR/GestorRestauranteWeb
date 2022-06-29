using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class MenuController : Controller
    {


        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(Models.Platillo PlatilloData)
        {
            GestorDeRestaurante.Model.Platillo elPlatillo = new Model.Platillo();

            elPlatillo.Nombre = PlatilloData.Nombre;
            elPlatillo.Categoria = (Model.Categoria)PlatilloData.Categoria;
            elPlatillo.Precio = PlatilloData.Precio;

            if (elPlatillo.Imagen.Length > 0)
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

        }//Fin metodod
        



    }//Fin clase
}
