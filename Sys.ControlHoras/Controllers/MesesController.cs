using Model;
using Sys.ControlTiempo.Helpers;
using Sys.Produccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Controllers
{
    public class MesesController : BaseApplicationController
    {
        ControlHoraEntities db = new ControlHoraEntities();
        public ActionResult Index()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult Agregar()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            string dato = db.MesProceso.OrderByDescending(X => X.AnioMes).Select(X => X.AnioMes).FirstOrDefault();
            string date_string = string.Format("{0}/{1}/01", dato.Substring(0,4), dato.Substring(4, 2));
            DateTime date = Convert.ToDateTime(date_string);
            DateTime newDate = date.AddMonths(1);
            MesProceso mesProceso = new MesProceso 
            {
                AnioMes = newDate.ToString("yyyy/MM/dd/").Replace("-","").Substring(0,6),
                Estado = 2
            };
            db.MesProceso.Add(mesProceso);
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        public ActionResult Activar(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var obj = db.MesProceso.Where(X => X.IdMesProceso == Id && X.Estado != 1).First();
            if (obj != null)
            {
                try
                {
                    obj.Estado = 1;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new Response { IsSuccess = true, Message = "Se ha activado el mes correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "El mes ya se encuentra activado" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se ha encontrado el mes para activar" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Suspender(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var obj = db.MesProceso.Where(X => X.IdMesProceso == Id && X.Estado != 2).First();
            if (obj != null)
            {
                try
                {
                    obj.Estado = 2;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new Response { IsSuccess = true, Message = "Se ha suspendido el mes correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "El mes ya se encuentra suspendido" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se ha encontrado el mes para suspender" }, JsonRequestBehavior.AllowGet);
        }        
        
        public ActionResult Finalizar(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var obj = db.MesProceso.Where(X => X.IdMesProceso == Id && X.Estado != 0).First();
            if (obj != null)
            {
                try
                {
                    obj.Estado = 0;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new Response { IsSuccess = true, Message = "Se ha finalizado el mes correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "El mes ya se encuentra finalizado" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se ha encontrado el mes para finalizar" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TraerTodo(string Anio)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            List<MesProceso> list = new List<MesProceso>();
            if (Anio != null)
            {
                list = db.MesProceso.Where(X => X.AnioMes.ToUpper().Contains(Anio)).OrderBy(X => X.AnioMes).ToList();
            }
            else
            {
                list = db.MesProceso.OrderBy(X => X.AnioMes).ToList();
            }
            return View(list);
        }

    }
}