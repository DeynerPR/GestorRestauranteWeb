using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeRestaurante.UI.Controllers
{
    public class IngredienteController : Controller
    {
        // GET: IngredienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: IngredienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IngredienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
