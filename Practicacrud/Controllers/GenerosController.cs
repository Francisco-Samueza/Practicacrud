using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practicacrud.Data;
using Practicacrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicacrud.Controllers
{
    public class GenerosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        // GET: GenerosController
        public ActionResult Index()
        {
            List<Genero> lstgeneros = _dbContext.Generos.ToList();
            return View(lstgeneros);
        }

        // GET: GenerosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GenerosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerosController/Create
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

        // GET: GenerosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenerosController/Edit/5
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

        // GET: GenerosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenerosController/Delete/5
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
