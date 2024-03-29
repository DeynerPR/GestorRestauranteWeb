﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GestorDeRestaurante.UI.Controllers
{
    public class IngredienteController : Controller
    {

        //GET: IngredienteController
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
        }//Fin 



        //GET: IngredienteController
        public async Task<IActionResult> Detalle(int Id)
        {
            Model.DetalleDelIngrediente elDetalleDelIngrediente;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Ingrediente/ObtengaElDetalleDelIngrediente", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                elDetalleDelIngrediente = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.DetalleDelIngrediente>(apiResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(elDetalleDelIngrediente);

        }//Fin 



        //POST: IngredienteController
        public ActionResult Agregar()
        {
            return View();
        }//Fin 


        //POST: IngredienteController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Models.Ingrediente ingrediente)
        {
            try
            {
                Model.Ingrediente elIngrediente = new Model.Ingrediente();

                elIngrediente.Id = ingrediente.Id;
                elIngrediente.Nombre = ingrediente.Nombre;

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elIngrediente);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7181/api/Ingrediente/AgregueElIngrediente", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }//Fin 



        //GET: IngredienteController
        public async Task<IActionResult> Editar(int Id)
        {

            Model.Ingrediente elIngrediente;

            try
            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {
                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7181/api/Ingrediente/ObtenerIngredientePorId", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elIngrediente = JsonConvert.DeserializeObject<GestorDeRestaurante.Model.Ingrediente>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(elIngrediente);
        
        }//Fin 



        //POST: IngredienteController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Model.Ingrediente ingrediente)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(ingrediente);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7181/api/Ingrediente/EditarIngrediente", byteContent);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();

            }
        }//Fin 



    }//FIN CLASE

}
