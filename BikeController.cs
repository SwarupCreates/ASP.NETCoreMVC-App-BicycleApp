using BicycleApp_MVC.WebApp.Models;
using BicycleApp_MVC.WebApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BicycleApp_MVC.WebApp.Controllers
{
    public class BicyclesController : Controller
    {
        private readonly IBicycleRepository _bicycleRepository;

        public BicyclesController(IBicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }

        // GET: Bicycles
        public IActionResult Index()
        {
            var bicycles = _bicycleRepository.GetAllBicycles();
            return View(bicycles);
        }

        // GET: Bicycles/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Bicycles/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                _bicycleRepository.AddBicycle(bicycle);
                return RedirectToAction(nameof(Index));
            }
            return View(bicycle);
        }

        // GET: Bicycles/Edit
        public IActionResult Edit(int id)
        {
            var bicycle = _bicycleRepository.GetBicycleById(id);
            if (bicycle == null)
            {
                return NotFound();
            }
            return View(bicycle);
        }

        // POST: Bicycles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Bicycle bicycle)
        {
            if (id != bicycle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bicycleRepository.EditBicycle(bicycle);
                return RedirectToAction(nameof(Index));
            }
            return View(bicycle);
        }

        // GET: Bicycles/Delete
        public IActionResult Delete(int id)
        {
            var bicycle = _bicycleRepository.GetBicycleById(id);
            if (bicycle == null)
            {
                return NotFound();
            }
            return View(bicycle);
        }

        // POST: Bicycles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bicycleRepository.DeleteBicycle(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Bicycles/Details
        public IActionResult Details(int id)
        {
            var bicycle = _bicycleRepository.GetBicycleById(id);
            if (bicycle == null)
            {
                return NotFound();
            }
            return View(bicycle);
        }

        // GET: Bicycles/DetailsByName
        public IActionResult DetailsByName(string name)
        {
            var bicycle = _bicycleRepository.GetBicycleByName(name);
            if (bicycle == null)
            {
                return NotFound();
            }
            return View("Details", bicycle); // Reuse the Details view
        }

    }
}
