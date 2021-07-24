using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practicacrud.Data;
using Practicacrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicacrud.Controllers
{
    [Authorize]
    public class GenerosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public GenerosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: GenerosController
        [Authorize(Roles = "Propietario,Cliente")]
        public ActionResult Index()
        {
            List<Genero> lstgeneros = _dbContext.Generos.ToList();
            return View(lstgeneros);
        }

        // GET: GenerosController/Details/5
        [Authorize(Roles = "Propietario,Cliente")]
        public ActionResult Details(int id)
        {
            Genero genero = _dbContext.Generos.FirstOrDefault(x => x.Codigo == id);
            return View();
        }

        // GET: GenerosController/Create
        [Authorize(Roles = "Propietario")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerosController/Create
        [Authorize(Roles = "Propietario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Genero genero)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _dbContext.Add(genero);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(genero);
            }
        }

        // GET: GenerosController/Edit/5
        [Authorize(Roles = "Propietario")]
        public ActionResult Edit(int id)
        {
            Genero genero = _dbContext.Generos.FirstOrDefault(x => x.Codigo == id);
            return View();
        }

        // POST: GenerosController/Edit/5
        [Authorize(Roles = "Propietario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genero genero)
        {
            if(id!=genero.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _dbContext.Update(genero);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(genero);
            }
        }

        //DESACTIVAR

        [Authorize(Roles = "Propietario")]
        public IActionResult Desactivar(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            Genero genero = _dbContext.Generos.Where(x => x.Codigo == id).FirstOrDefault();
            try
            {
                genero.Estado = 0;
                _dbContext.Update(genero);
                _dbContext.SaveChanges();

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //ACTIVAR

        [Authorize(Roles = "Propietario")]
        public IActionResult Activar(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            Genero genero = _dbContext.Generos.Where(x => x.Codigo == id).FirstOrDefault();
            try
            {
                genero.Estado = 1;
                _dbContext.Update(genero);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
