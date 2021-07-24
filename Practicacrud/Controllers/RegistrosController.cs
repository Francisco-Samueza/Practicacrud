using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practicacrud.Data;
using Practicacrud.Models;
using Practicacrud.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Practicacrud.Controllers
{
    [Authorize]
    public class RegistrosController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        public RegistrosController(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }
        //INDEX
        [Authorize(Roles = "Propietario,Cliente")]
        public IActionResult Index()
        {
            List<RegistroViewModel> registros = new List<RegistroViewModel>();
            registros = _applicationDb.Registro.Select(r => new RegistroViewModel
            {
                Codigo = r.Codigo,
                Nombre = r.Nombre,
                Apellido = r.Apellido,
                Estado = r.Estado,
                Direccion=r.Direccion,
                DescripcionGenero=r.CodigoGeneroNavigation.Descripcion
            }).ToList();
            return View(registros);
        }
        //CREAR

       [Authorize(Roles = "Propietario")]
        public IActionResult Create()
        {
            ViewData["CodigoGenero"] = new SelectList(_applicationDb.Generos.Where(a => a.Estado == 1).ToList(), "Codigo", "Descripcion");
            return View(); 
        }

        [Authorize(Roles = "Propietario")]
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
                ViewData["CodigoGenero"] = new SelectList(_applicationDb.Generos.Where(a => a.Estado == 1).ToList(), "Codigo", "Descripcion",registro.CodigoGenero);
                return View(registro);
            }
            return RedirectToAction("Index");
        }

        //EDITAR

        [Authorize(Roles = "Propietario")]
        public IActionResult Edit(int id )
        {
           
            if (id==0)
                return RedirectToAction("Index");
            Registro registro = _applicationDb.Registro.Where(s => s.Codigo == id).FirstOrDefault();
            if(registro==null)
                return RedirectToAction("Index");
            ViewData["CodigoGenero"] = new SelectList(_applicationDb.Generos.Where(a => a.Estado == 1).ToList(), "Codigo", "Descripcion",registro.CodigoGenero);
            return View(registro);
        }

        [Authorize(Roles = "Propietario")]
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
                ViewData["CodigoGenero"] = new SelectList(_applicationDb.Generos.Where(a => a.Estado == 1).ToList(), "Codigo", "Descripcion", registro.CodigoGenero);
                return View(registro);
            }
            return RedirectToAction("Index");
        }

        //DETALLES

        [Authorize(Roles = "Propietario")]
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

        [Authorize(Roles = "Propietario")]
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

        [Authorize(Roles = "Propietario")]
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
