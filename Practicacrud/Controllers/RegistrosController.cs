using Microsoft.AspNetCore.Mvc;
using Practicacrud.Data;
using Practicacrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicacrud.Controllers
{
    public class RegistrosController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        public RegistrosController(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }
        //INDEX
        public IActionResult Index()
        {
            List<Registro> registros = new List<Registro>();
            registros = _applicationDb.Registro.ToList();
            return View(registros);
        }
        //CREAR
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Registro registro)
        {
            try
            {
                _applicationDb.Add(registro);
                _applicationDb.SaveChanges();
            }
            catch (Exception)
            {
                return View(registro);
            }
            return RedirectToAction("Index");
        }
        //EDITAR
        public IActionResult Edit(int id )
        {
            if(id==0)
                return RedirectToAction("Index");
            Registro registro = _applicationDb.Registro.Where(s => s.Codigo == id).FirstOrDefault();
            if(registro==null)
                return RedirectToAction("Index");
            return View(registro);
        }
        [HttpPost]
        public IActionResult Edit(int id, Registro registro)
        {
            if (id != registro.Codigo)
                return RedirectToAction("Index");
            try
            {
                _applicationDb.Update(registro);
                _applicationDb.SaveChanges();
            }
            catch (Exception)
            {

                return View(registro);
            }
            return RedirectToAction("Index");
        }
        //DETALLES
        public IActionResult Details(int id)
        {
            if(id==0)
                return RedirectToAction("Index");
            Registro registro = _applicationDb.Registro.Where(x => x.Codigo == id).FirstOrDefault();
            if(registro==null)
                return RedirectToAction("Index");
            return View(registro);
        }
        //DESACTIVAR
        public IActionResult Desactivar(int id )
        {
            if(id==0)
                return RedirectToAction("Index");
            Registro registro = _applicationDb.Registro.Where(x => x.Codigo == id).FirstOrDefault();
            try
            {
                registro.Estado = 0;
                _applicationDb.Update(registro);
                _applicationDb.SaveChanges();

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //ACTIVAR
        public IActionResult Activar(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            Registro registro = _applicationDb.Registro.Where(x => x.Codigo == id).FirstOrDefault();
            try
            {
                registro.Estado = 1;
                _applicationDb.Update(registro);
                _applicationDb.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
