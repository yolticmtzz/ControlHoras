using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.Web.Mvc;
using Sys.Produccion.Models;
using Sys.Produccion.Helpers;
using EFRepository;
using Repository;
using Newtonsoft.Json;
using Sys.ControlTiempo.Helpers;

namespace Sys.ControlTiempo.Controllers
{
    public class EmpleadosController : BaseApplicationController
    {
        ControlHoraEntities db = new ControlHoraEntities();
        // GET: Empleados
        public ActionResult Index()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult Agregar(Empleado empleado)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            IRepository repository = new Model.Repository();

            if (db.Empleado.Where(X => X.Vigente == true && X.Rut.ToUpper().Equals(empleado.Rut.ToUpper())).Count() > 0)
            {
                return Json(new Response { IsSuccess = false, Message = "Ya existe un empleado con ese rut" }, JsonRequestBehavior.AllowGet);
            }
            if (db.Empleado.Where(X => X.Vigente == true && X.Correo.ToUpper().Equals(empleado.Correo.ToUpper())).Count() > 0)
            {
                return Json(new Response { IsSuccess = false, Message = "Ya existe un empleado con ese correo" }, JsonRequestBehavior.AllowGet);
            }

            string strPass = CryproHelper.ComputeHash(empleado.Clave, CryproHelper.Supported_HA.SHA512, null);
            empleado.Clave = strPass;
            empleado.Vigente = true;
            var obj = db.Empleado.Add(empleado);
            try
            {

                if (db.SaveChanges() > 0)
                {
                    return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido agregado satisfactoriamente", empleado.Nombre) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new Response { IsSuccess = true, Message = string.Format(e.Message) }, JsonRequestBehavior.AllowGet);
            }




            return Json(new Response { IsSuccess = false, Message = "No se pudo agregar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(FormCollection empleado)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            string rut = empleado["Rut"];
            string correo = empleado["Correo"];
            int id = Int32.Parse(empleado["IdEmpleado"]);
            var obj = db.Empleado.Where(X => X.Vigente == true && X.IdEmpleado == id).First();
            string clave = empleado["Clave"].ToString();
            if (obj != null)
            {
                obj.IdPerfil = Int32.Parse(empleado["IdPerfil"]);
                obj.Nombre = empleado["Nombre"].ToString().ToUpper();
                obj.Rut = empleado["Rut"].ToString().ToUpper();
                if (!string.IsNullOrEmpty(clave))
                {
                    obj.Clave = CryproHelper.ComputeHash(clave, CryproHelper.Supported_HA.SHA512, null);
                }

                obj.Correo = empleado["Correo"].ToString().ToUpper();
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
            return Json(new Response { IsSuccess = false, Message = "No se pudo editar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var obj = db.Empleado.Where(X => X.Vigente == true && X.IdEmpleado == Id).First();
            if (obj != null)
            {
                obj.Vigente = false;
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
            List<Empleado> list = db.Empleado.Where(X => X.Vigente == true).OrderBy(X => X.Nombre).ToList();
            return View(list);
        }

        public ActionResult Traer(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            IRepository repository = new Model.Repository();
            var obj = repository.FindEntity<Empleado>(c => c.IdEmpleado == Id && c.Vigente == true);
            if (obj != null)
            {
                return Json(new Response { IsSuccess = true, Result = obj }, JsonRequestBehavior.AllowGet);
            }

            return Json(new Response { IsSuccess = false, Message = "No se a podido traer al usuario" }, JsonRequestBehavior.AllowGet);

        }


    }
}