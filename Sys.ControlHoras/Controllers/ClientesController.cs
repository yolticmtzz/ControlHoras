using Model;
using Repository;
using Sys.ControlTiempo.Helpers;
using Sys.Produccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Controllers
{
    public class ClientesController : BaseApplicationController
    {
        ControlHoraEntities db = new ControlHoraEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Cliente cliente)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            if (!string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                cliente.Nombre = cliente.Nombre.ToUpper();
                cliente.Vigente = true;
                try
                {
                    db.Cliente.Add(cliente);
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new Response { IsSuccess = true, Message = "Se ha agregado el cliente correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo agregar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Editar(Cliente cliente)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");

            var objEdit = db.Cliente.Where(X => X.Vigente == true && X.IdCliente == cliente.IdCliente).First();
            if (objEdit != null)
            {
                objEdit.Nombre = cliente.Nombre.ToUpper();
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new Response { IsSuccess = true, Message = "El registro se ha modificado correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No existen cambios, no se ha editado el registro" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo modificar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var cliente = db.Cliente.Where(X => X.Vigente == true && X.IdCliente == Id).First();
            if (cliente != null)
            {
                cliente.Vigente = false;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new Response { IsSuccess = true, Message = "El registro se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No existen cambios, no se ha eliminado el registro" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo eliminar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TraerTodo()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            List<Cliente> list = db.Cliente.Where(X => X.Vigente == true).OrderBy(X => X.Nombre).ToList();
            return View(list);
        }

        public ActionResult Traer(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");

            IRepository repository = new Model.Repository();
            var obj = repository.FindEntity<Cliente>(c => c.IdCliente == Id && c.Vigente == true);
            if (obj != null)
            {
                return Json(new Response { IsSuccess = true, Result = obj }, JsonRequestBehavior.AllowGet);
            }

            return Json(new Response { IsSuccess = false, Message = "No se a podido traer al cliente" }, JsonRequestBehavior.AllowGet);
        }

    }
}